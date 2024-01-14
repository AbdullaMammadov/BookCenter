using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.AdminArea.Controllers
{
    public class Dashboard : Controller
    {
        [Area("AdminArea")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
