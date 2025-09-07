
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    public class ClaimController : Controller
    {
        [HttpGet]
        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Claim model)
        {
            if (ModelState.IsValid)
            {
                // Example: save uploaded file
                if (model.Document != null && model.Document.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/uploads", model.Document.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Document.CopyTo(stream);
                    }
                }

                // Save to database logic here (e.g. EF Core)

                ViewBag.Message = "Claim submitted successfully!";
                return View();
            }
            return View(model);
        }
    }
}
