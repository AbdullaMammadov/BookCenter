using FinalProject.DAL;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalProject.Controllers
{
    public class BookController : Controller
    {
        private AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult SearchProduct(string? text)
        {
            if (text!=null)
            {
                try
                {
                    var products = _context.Books
                        .Where(p => p.Name.ToLower().Contains(text.ToLower()))
                         .Take(4)
                       .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.Category)

                        .ToList();

                    return PartialView("_searchPartial", products);
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);

                    // Optionally, rethrow the exception to let it propagate
                    throw;
                }
            }
            else
            {
                return View();
            }
        }
        public IActionResult Detail(int? id)
        {
            var selectedBook = _context.Books.FirstOrDefault(b => b.Id == id);
            
            var relatedProducts = _context.Books
                 .Where(b => b.CategoryId == selectedBook.CategoryId && b.Id != selectedBook.Id)
                 .OrderByDescending(s => s.Id)
                 .Take(6)
                 .ToList();
            var categoryName = _context.Categories
           .Where(c => c.Id == selectedBook.CategoryId)
           .Select(c => c.Name)
           .FirstOrDefault();
            var authorNames = _context.BookAuthors
           .Where(ba => ba.BookId == selectedBook.Id)
           .Select(ba => ba.Author.Name)
           .ToList();

          
            var genreNames = _context.BookGenres
                .Where(bg => bg.BookId == selectedBook.Id)
                .Select(bg => bg.Genre.Name)
                .ToList();
            var viewModel = new BookVM
            {
                Book = selectedBook,
                Books = relatedProducts,
                CategoryName = categoryName,
                  AuthorNames = authorNames,
                GenreNames = genreNames
            };
   
            return View(viewModel);


        }
    }
}
