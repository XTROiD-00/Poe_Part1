using Microsoft.AspNetCore.Mvc;
using Poe_Part1.Models;
using System.IO;

namespace Poe_Part1.Controllers
{
    public class ClaimController : Controller
    {
        // Create an instance of ClaimsQueries for interacting with the database
        private readonly ClaimsQueries _claimsDb;

        public ClaimController()
        {
            _claimsDb = new ClaimsQueries(); // Instantiate ClaimsQueries here
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

            Console.WriteLine(claim.ClaimId);

            Console.WriteLine(claim.FacultyName);

            Console.WriteLine(claim.ModuleName );

            Console.WriteLine(claim.Sessions );

            Console.WriteLine(claim.Hours );    

            Console.WriteLine(claim.Rate );

            Console.WriteLine(claim.TotalAmount );

            Console.WriteLine(claim.DocumentPath );



            if (!ModelState.IsValid)
            {
                // Handle file upload if provided
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    // Ensure the uploads directory exists
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var filePath = Path.Combine(uploadsFolder, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    claim.DocumentPath = "/uploads/" + file.FileName; // Save document path in the claim object
                }

                // Calculate the total amount for the claim
                claim.TotalAmount = claim.Sessions * claim.Hours * claim.Rate;

                // Save the claim in the database using ClaimsQueries
                _claimsDb.StoreClaim(claim);

                // Redirect to Success action after successful submission
                TempData["ClaimMessage"] = "Claim submitted successfully!";
                return RedirectToAction("Submit");
            }
            else
            {
                Console.WriteLine("error");

            }

            // If validation fails, re-render the form with errors
            return View(claim);
        }

        // Action to show success message after submitting a claim
        public IActionResult Success()
        {
            return View();
        }

        // GET action for tracking claims
        [HttpGet]
        public IActionResult Track_Claim()
        {
            // Optionally, you can pass the success message after submission
            if ("ClaimMessage" != null)
            {
                ViewBag.ClaimMessage = TempData["ClaimMessage"];
            }

            // Retrieve all claims (you may add a method for that in ClaimsQueries if needed)
            return View(); // You can pass claims data to the view if needed
        }
    }
}
