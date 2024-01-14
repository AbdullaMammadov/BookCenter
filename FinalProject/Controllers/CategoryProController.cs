using FinalProject.DAL;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Controllers
{
    public class CategoryProController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryProController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _context.CategoryPros
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);


            if (category == null)
            {
                return NotFound();
            }


            var categoryVM = new CategoryVM
            {
                CategoryPro = category,
                Activities = _context.Activities
                .Where(a => a.IsTedbir == true)
                .Take(3)
                .ToList(),
                
                
            };


            return View(categoryVM);
        }
    }
}
