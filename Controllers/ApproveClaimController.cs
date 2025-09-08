using Microsoft.AspNetCore.Mvc;
using Poe_Part1.Models;
using System;
using System.Collections.Generic;

namespace Poe_Part1.Controllers
{
    public class ClaimsController : Controller
    {
        // Mock Data (replace with DB context later)
        public IActionResult Index()
        {
            var claims = new List<Claim>
            {
                new Claim { ClaimId = 101, ModuleId = 501, Sessions = 15, Hours = 150, Rate = 15, TotalAmount = 2250,
                    Status = "Pre-approved", SubmittedDate = new DateTime(2025, 9, 1), SubmittedTime="08:15",
                    PreApprovedDate = new DateTime(2025, 9, 2), PreApprovedTime="10:00", FinalizedDate = null, Document="claim1.pdf" },

                new Claim { ClaimId = 102, ModuleId = 502, Sessions = 12, Hours = 120, Rate = 20, TotalAmount = 2400,
                    Status = "Approved", SubmittedDate = new DateTime(2025, 9, 2), SubmittedTime="09:10",
                    PreApprovedDate = new DateTime(2025, 9, 3), PreApprovedTime="12:00", FinalizedDate = new DateTime(2025, 9, 5),
                    Document="claim2.pdf" }
            };

            return View(claims);
        }

        public IActionResult Preview(int id)
        {
            // Fetch claim details by id
            return Content($"Preview Claim {id}");
        }

        public IActionResult Reject(int id)
        {
            // Logic for rejecting claim
            return Content($"Rejected Claim {id}");
        }
    }
}
