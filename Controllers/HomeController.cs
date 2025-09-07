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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy(string role )
        {
            ViewBag.role = role;
            return View();
        }

        public IActionResult Lecturer()
        {
            return View(); // This will look for Views/Home/Lecturer.cshtml
        }

        public IActionResult Project_Manager()
        {
            return View(); // This will look for Views/Home/Lecturer.cshtml
        }

        public IActionResult Program_coordinator()
        {
            return View(); // This will look for Views/Home/Lecturer.cshtml
        }

        public IActionResult Claim ()
        {
            return View(); // This will look for Views/Home/Lecturer.cshtml
        }
        public IActionResult TrackClaim()
        {
            return View(); // This will look for Views/Home/Lecturer.cshtml
        }
        public IActionResult Pre_Approve()
        {
            return View(); // This will look for Views/Home/Lecturer.cshtml
        }
        public IActionResult Approve()
        {
            return View(); // This will look for Views/Home/Lecturer.cshtml
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
