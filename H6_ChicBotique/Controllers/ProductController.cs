using Microsoft.AspNetCore.Mvc;

namespace H6_ChicBotique.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
