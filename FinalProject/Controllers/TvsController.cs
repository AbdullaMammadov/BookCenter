using FinalProject.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class TvsController : Controller
    {
        private readonly AppDbContext _context;
        public TvsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var settings = _context.Settings.ToDictionary(s => s.Key, s => s.Value);
            return View(settings);
        }
    }
}
