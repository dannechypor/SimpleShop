using Microsoft.AspNetCore.Mvc;

namespace ForGoodTime.Controllers
{
    public class HomeController() : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}