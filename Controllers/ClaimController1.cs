using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem;
using System;
using System.Collections.Generic;
using Poe_Part1.Models;
using Poe_Part1;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        public IActionResult Track_Claim()
        {
            // Mock data (normally comes from DB)
            var claims = new List<Track_Claim>
            {
                new Track_Claim { UserId=101, ModuleId=501, Sessions=10, Hours=15, Rate=150, TotalAmount=2250, Status="Pending", SubmittedDate=DateTime.Parse("2025-09-01"), SubmittedTime="09:15", Document="claim1.pdf" },
                new Track_Claim { UserId=102, ModuleId=502, Sessions=8, Hours=12, Rate=200, TotalAmount=2400, Status="Pre-approved", SubmittedDate=DateTime.Parse("2025-09-02"), SubmittedTime="10:00", PreApprovedDate=DateTime.Parse("2025-09-03"), PreApprovedTime="12:00", Document="claim2.pdf" },
                new Track_Claim { UserId=103, ModuleId=503, Sessions=12, Hours=18, Rate=120, TotalAmount=2160, Status="Approved", SubmittedDate=DateTime.Parse("2025-09-05"), SubmittedTime="14:30", PreApprovedDate=DateTime.Parse("2025-09-06"), PreApprovedTime="09:00", FinalizedDate=DateTime.Parse("2025-09-07"), FinalizedTime="11:00", Document="claim3.pdf" },
                new Track_Claim { UserId=104, ModuleId=504, Sessions=6, Hours=9, Rate=180, TotalAmount=1620, Status="Pending", SubmittedDate=DateTime.Parse("2025-09-08"), SubmittedTime="08:45", Document="claim4.pdf" },
                new Track_Claim { UserId=105, ModuleId=505, Sessions=9, Hours=14, Rate=160, TotalAmount=2240, Status="Pre-approved", SubmittedDate=DateTime.Parse("2025-09-09"), SubmittedTime="11:30", PreApprovedDate=DateTime.Parse("2025-09-10"), PreApprovedTime="10:00", Document="claim5.pdf" },
                new Track_Claim { UserId=106, ModuleId=506, Sessions=11, Hours=20, Rate=140, TotalAmount=2800, Status="Approved", SubmittedDate=DateTime.Parse("2025-09-11"), SubmittedTime="13:15", PreApprovedDate=DateTime.Parse("2025-09-12"), PreApprovedTime="09:30", FinalizedDate=DateTime.Parse("2025-09-13"), FinalizedTime="10:45", Document="claim6.pdf" }
            };

            return View(claims);
        }
    }
}
