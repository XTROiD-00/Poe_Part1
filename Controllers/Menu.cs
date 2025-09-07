using Microsoft.AspNetCore.Mvc;

namespace Poe_Part1.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Privacy/Index.cshtml");
        }
    }
}
