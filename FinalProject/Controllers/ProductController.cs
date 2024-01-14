using FinalProject.DAL;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Detail(int? id)
        {
            var selectedBook = _context.Products.FirstOrDefault(b => b.Id == id);

            var relatedProducts = _context.Products
                 .Where(b => b.CategoryProId == selectedBook.CategoryProId && b.Id != selectedBook.Id)
                 .OrderByDescending(s => s.Id)
            .Take(6)

                 .ToList();
            var categoryName = _context.CategoryPros
           .Where(c => c.Id == selectedBook.CategoryProId)
            .Select(c => c.Name)
           .FirstOrDefault();
        

            // Kitabın türlerinin isimlerini al
        
            var viewModel = new BookVM
            {
                Prodcuct=selectedBook,
                Products = relatedProducts,
                CategoryName = categoryName,
          
            };

            return View(viewModel);


        }
    }
}
