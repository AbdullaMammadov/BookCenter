using FinalProject.DAL;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalProject.Controllers
{
    public class ActivityController : Controller
    {
        private AppDbContext _context;

        public ActivityController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Tedbir(int? id)
        {
            ActivityVM activityVM = new ActivityVM();
             activityVM.Tedbirs=_context.Activities
            .Where(a => a.IsTedbir == true)
            .Where(a => a.Id!=id)
            .OrderByDescending(s => s.Id)
            .Take(3)
            .ToList();
             activityVM.Tedbir=_context.Activities
           . SingleOrDefault(a => a.Id == id);
            return View(activityVM);
        }
        public IActionResult Lahiye(int? id)
        {
            ActivityVM activityVM = new ActivityVM();
            activityVM.Lahiyes = _context.Activities
           .Where(a => a.IsTedbir == false )
           .Where(a => a.Id != id)
           .OrderByDescending(s => s.Id)
           .Take(3)
           .ToList();
            activityVM.Lahiye = _context.Activities
          .SingleOrDefault(a => a.Id == id);
            return View(activityVM);
        }
        public IActionResult Tedbirs()
        {
              ActivityVM activityVM=new ActivityVM();
              activityVM.Tedbirs = _context.Activities
               .Where(a => a.IsTedbir == true)
               .OrderByDescending(s => s.Id)
               .ToList();
            activityVM.books=_context.Books
                .Where(b=>b.DiscountPrice!=null)
                .Take(6)
                .ToList() ;
            return View(activityVM);

        }
        public IActionResult Lahiyes()
        {
            ActivityVM activityVM = new ActivityVM();
            activityVM.Lahiyes = _context.Activities
             .Where(a => a.IsTedbir == false)
             .OrderByDescending(s => s.Id)
             .ToList();
            activityVM.books = _context.Books
                .Where(b => b.DiscountPrice != null)
                .Take(6)
                .ToList();
            return View(activityVM);

        }
    }
}
