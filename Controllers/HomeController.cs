using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Poe_Part1.Models;

namespace Poe_Part1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var db = new all_queries();
            db.creates_table();

            // Call to create claims table if not exists
            var claimsDb = new ClaimsQueries();
            claimsDb.CreateClaimsTable();  // This ensures the claims table is created if it's not already.

            return View();
        }

        [HttpPost]
        public IActionResult Index(Register user)
        {
            if (ModelState.IsValid)
            {
                var db = new all_queries();
                db.store_user(user.name, user.surname, user.email, user.password);
                TempData["SuccessMessage"] = "Registration successful! Please login.";
                return RedirectToAction("Privacy");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Privacy(string role = null)
        {
            ViewBag.role = role;
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.Message = TempData["SuccessMessage"];
            }

            return View();
        }

        [HttpPost]
        public IActionResult Privacy(login user, string role)
        {
            if (ModelState.IsValid)
            {
                var db = new all_queries();
                bool found = db.search_user(user.name, user.surname, user.email, user.password);

                if (found)
                {
                    db.store_login(user.name, user.surname, user.email, user.password);
                    return role switch
                    {
                        "Lecturer" => RedirectToAction("Lecturer"),
                        "Project Manager" => RedirectToAction("Project_Manager"),
                        "Program coordinator" => RedirectToAction("Program_coordinator"),
                        _ => RedirectToAction("Index")
                    };
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login credentials.");
                }
            }

            return View(user);
        }

        // Claims-related actions

       /* [HttpGet]
        public IActionResult Claim()
        {
            // Return the Claim View for users to submit a claim
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClaim(string facultyName, string moduleName, int sessions, TimeSpan hours, decimal price)
        {
            if (ModelState.IsValid)
            {
                var claimsDb = new ClaimsQueries();
                claimsDb.StoreClaim(facultyName, moduleName, sessions, hours, price);

                TempData["ClaimMessage"] = "Claim submitted successfully!";
                return RedirectToAction("Track_Claim");
            }

            return View("Claim");
        } */

        [HttpGet]
        public IActionResult Track_Claim()
        {
            if (TempData["ClaimMessage"] != null)
            {
                ViewBag.ClaimMessage = TempData["ClaimMessage"];
            }

            return View();
        }

        // Other actions
        public IActionResult Lecturer() => View();
        public IActionResult Project_Manager() => View();
        public IActionResult Program_coordinator() => View();
        public IActionResult Pre_Approve() => View();
        public IActionResult ApproveClaimIndex() => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
