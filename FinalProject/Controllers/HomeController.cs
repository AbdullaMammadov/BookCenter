using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           
            HomeVM homeVM = new();
            homeVM.Sliders = _context.Sliders
            .OrderByDescending(s => s.Id)
            .Take(5)
            .ToList();
            homeVM.Tedbirs = _context.Activities
            .Where(a => a.IsTedbir==true) 
            .OrderByDescending(a => a.Id)
            .Take(3)
            .ToList();
            homeVM.Lahiyes = _context.Activities
            .Where(a => a.IsTedbir == false)
            .OrderByDescending(a => a.Id)
            .Take(3)
            .ToList();

            homeVM.Books = _context.Books
                .OrderByDescending(c => c.Id)
                .Where(b => b.DiscountPrice != null)
                .Take(6)
                .ToList();
            homeVM.Categories = _context.Categories
          .Where(c => c.Books.Any())
          .OrderByDescending(c => c.Id)
          .Take(6)
          .Select(c => new Category
          {
              Id = c.Id,
              Name = c.Name,
              Books = c.Books
                  .OrderByDescending(b => b.Id)
                  .Take(6)
                  .ToList()
          })
          .ToList();
            homeVM.LastBooks = _context.Books
               .OrderByDescending(c => c.Id)
                 .Take(6)
               .ToList();
              

            return View(homeVM);
        }
    }
}
