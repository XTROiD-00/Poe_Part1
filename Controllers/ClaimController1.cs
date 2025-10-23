using System.IO;
using Microsoft.AspNetCore.Mvc;
using Poe_Part1.Data;
using Poe_Part1.Models;
using System.Linq;

namespace Poe_Part1.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public ClaimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET action for submitting a claim
        [HttpGet]
        public IActionResult Submit()
        {
            return View();
        }

        // POST action for submitting a claim
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

                    // Ensure the uploads folder exists
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var filePath = Path.Combine(uploadsFolder, file.FileName);

                    // Save the file to the uploads folder
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        file.CopyTo(stream);

                    // Store the file path in the claim
                    claim.DocumentPath = "/uploads/" + file.FileName;
                }

                // Add the claim to the database
                _context.Claims.Add(claim);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }

            // If validation failed, re-render the form
            return View(claim);
        }

        // Action to show success message
        public IActionResult Success()
        {
            return View();  // Create a Success.cshtml with a success message
        }

        // GET action for tracking claims
        [HttpGet]
        public IActionResult Track_Claim()
        {
            var claims = _context.Claims.ToList();  // Fetch all claims from the database
            return View(claims);  // Pass the claims data to the view
        }
    }
}
