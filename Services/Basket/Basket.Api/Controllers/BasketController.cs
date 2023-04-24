using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
