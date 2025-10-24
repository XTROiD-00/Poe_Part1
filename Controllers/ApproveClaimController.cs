using Microsoft.AspNetCore.Mvc;
using Poe_Part1.Models;
using System;

public class ClaimsCApproontroller : Controller
{
    all_queries _query = new all_queries();

    // ✅ Display all claims
    [HttpGet]
    public IActionResult MyClaims()
    {
        var claims = _query.get_all_claims();
        return View(claims);
    }

    // ✅ Display single claim details
    [HttpGet]
    public IActionResult Details(int id)
    {
        var claims = _query.get_all_claims();
        var claim = claims.Find(c => c.ClaimId == id);
        if (claim == null)
            return NotFound();

        return View(claim);
    }

    // ✅ PC: Pre-Approve claim
    public IActionResult PreApprove(int id)
    {
        _query.update_claim_status(id, "Pre-Approved");
        TempData["SuccessMessage"] = "Claim pre-approved successfully.";
        return RedirectToAction("MyClaims");
    }

    // ✅ PM: Approve claim
    public IActionResult Approve(int id)
    {
        _query.update_claim_status(id, "Approved");
        TempData["SuccessMessage"] = "Claim approved successfully.";
        return RedirectToAction("MyClaims");
    }

    // ✅ PC or PM: Reject claim
    public IActionResult Reject(int id)
    {
        _query.update_claim_status(id, "Rejected");
        TempData["SuccessMessage"] = "Claim rejected.";
        return RedirectToAction("MyClaims");
    }
}
