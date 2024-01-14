using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Settings.ToDictionary(s => s.Key, s => s.Value));
        }

        public IActionResult Update(string key)
        {
            if (key == null)
            {
                return NotFound();
            }

            var setting = _context.Settings.FirstOrDefault(s => s.Key == key);

            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(string key, string value)
        {
            if (key == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings.FirstOrDefaultAsync(s => s.Key == key);

            if (setting == null)
            {
                return NotFound();
            }

            setting.Value = value;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
