using Microsoft.AspNetCore.Mvc;

namespace PlantsShop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
