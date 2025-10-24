using Microsoft.AspNetCore.Mvc;
using Poe_Part1.Models;
using System;
using System.Collections.Generic;

namespace Poe_Part1.Controllers
{
    public class ApproveClaimController : Controller
    {
        private static List<ApproveClaim> claims = new List<ApproveClaim>
        {
            new ApproveClaim
            {
                ClaimId = 1,
                ModuleId = "502",
                Sessions = 3,
                Hours = 15,
                Rate = 150,
                Status = "Pending",
                SubmittedDate = DateTime.Parse("2025-09-01"),
                SubmittedTime = "08:15",
                Document = "claim1.pdf"
            } };

        public IActionResult ApproveClaimIndex()
        {
            return View(claims);
        }
    }
}