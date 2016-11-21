using GenFu;
using Microsoft.AspNetCore.Mvc;
using pugzore.website.Models;

namespace pugzore.website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData.Add("Title", "Welcome to Pugzor!");
            ModelState.AddModelError("model", "An error has occurred");
            return View(new { People = A.ListOf<Person>() });
        }

        public IActionResult SharedLocation()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View("~/ViewsSoCrazy/crazy.pug");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
