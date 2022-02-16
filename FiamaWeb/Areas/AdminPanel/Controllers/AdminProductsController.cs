using Microsoft.AspNetCore.Mvc;

namespace FiamaWeb.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    public class AdminProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
