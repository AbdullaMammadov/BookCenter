using FinalProject.DAL;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _context.Categories
                .Include(c => c.Books) 
                .FirstOrDefault(c => c.Id == id);
            

            if (category == null)
            {
                return NotFound();
            }


            var categoryVM = new CategoryVM
            {
                Category = category,
                Activities = _context.Activities
                .Where(a => a.IsTedbir == true)
                .Take(3)
                .ToList()
            };
          

            return View(categoryVM);
        }
    }
}
