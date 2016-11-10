using Microsoft.AspNetCore.Mvc;

namespace pugzore.website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Model"] = new { name = "blragjkfdkjfsdsa" };
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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
