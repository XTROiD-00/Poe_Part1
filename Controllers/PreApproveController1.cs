using Microsoft.AspNetCore.Mvc;
using Poe_Part1.Data;
using Poe_Part1.Models;

namespace Poe_Part1.Controllers
{

        public class ClaimController : Controller
        {
            private readonly ApplicationDbContext _context;

            public ClaimController(ApplicationDbContext context)
            {
                _context = context;
            }

            // Display all claims
            [HttpGet]
            public IActionResult MyClaims()
            {
                var claims = _context.Claims.ToList();
                return View(claims); // Passing the claims list to the view
            }

            // Pre-approve claim
            [HttpPost]
            public IActionResult PreApprove(int id)
            {
                var claim = _context.Claims.Find(id);
                if (claim != null)
                {
                    claim.Status = "Pre-Approved";
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Claim pre-approved successfully.";
                }
                return RedirectToAction("MyClaims");
            }

            // Reject claim
            [HttpPost]
            public IActionResult Reject(int id)
            {
                var claim = _context.Claims.Find(id);
                if (claim != null)
                {
                    claim.Status = "Rejected";
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Claim rejected.";
                }
                return RedirectToAction("MyClaims");
            }
        }
    
}