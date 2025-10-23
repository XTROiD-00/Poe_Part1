using System.IO;
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

        // GET action for submitting claim
        [HttpGet]
        public IActionResult Submit()
        {
            return View();
        }

        // POST action for submitting claim
        [HttpPost]
        public IActionResult Submit(Claim claim)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var filePath = Path.Combine(uploadsFolder, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                        file.CopyTo(stream);

                    claim.DocumentPath = "/uploads/" + file.FileName;
                }

                // Add claim to the database
                _context.Claims.Add(claim);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }

            // If validation failed, re-render the form
            return View(claim);
        }

        // Success action to show a confirmation message
        public IActionResult Success()
        {
            return View(); // Create Success.cshtml with a success message
        }

        // GET action for tracking claims
        [HttpGet]
        public IActionResult Track_Claim()
        {
            var claims = _context.Claims.ToList(); // Get all claims from database
            return View(claims); // Pass claims data to view
        }
    }
}
