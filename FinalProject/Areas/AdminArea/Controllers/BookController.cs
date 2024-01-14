using FinalProject.DAL;
using FinalProject.Extension;
using FinalProject.Models;
using FinalProject.ViewModel.Admin.Book;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using FinalProject.ViewModel;
///2
namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BookController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
      

        public BookController(AppDbContext context, IWebHostEnvironment webHostEnvironment,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }



        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var book = _context.Books.FirstOrDefault(c => c.Id == id);
            if (book == null) return NotFound();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var books = _context.Books
                .Include(b=>b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.Category)

                .ToList();
            return View(books);
        }
        public IActionResult Create()
        {
            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "Name");
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name");
            ViewBag.categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
          
        }
        [Area("AdminArea")]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAsync(CreateBookVM createBookVM)
        {
            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "Name");
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name");


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Photo", "bosh qoyma");
                return View();

            }
            if (createBookVM.Photo.IsImage())
            {

                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (createBookVM.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "sekil boyukdur");
                return View();
            }
        
            Book newBook = new();

            newBook.ImgUrl = createBookVM.Photo.SaveImage(webHostEnvironment, "assets/img");

            foreach (int authorId in createBookVM.AuthorIds)
            {
                BookAuthor bookAuthor = new();
                bookAuthor.AuthorId = authorId;
                bookAuthor.BookId = newBook.Id;
                newBook.BookAuthors.Add(bookAuthor);

            }

            foreach (int genreId in createBookVM.GenreIds)
            {
                BookGenre bookGenre = new();
                bookGenre.GenreId = genreId;
                bookGenre.BookId = newBook.Id;
                newBook.BookGenres.Add(bookGenre);

            }
            newBook.CategoryId = createBookVM.CategoryId;
            newBook.Price = createBookVM.Price;
            newBook.DiscountPrice = createBookVM.DiscountPrice;
            newBook.Name = createBookVM.Name;
            newBook.AddedBy = createBookVM.AddedBy;
            newBook.CreatedAt = DateTime.Now;
            _context.Books.Add(newBook);
            _context.SaveChanges();
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var link = Url.Action("Detail", "Book", new { Id = newBook.Id }, Request.Scheme, Request.Host.ToString());
                var newLink = link.Replace("/AdminArea/", "/");
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("mammadova.nazrin2004@gmail.com", "Book Center");
                mail.To.Add(new MailAddress(user.Email));
                mail.Subject = "New Product";
                mail.Body = $"<a href={newLink}> Yeni Kitabımızı görmək üçün linkı linkə dahil olun</a>";
                mail.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("mammadova.nazrin2004@gmail.com", "cpai qbgb ybst rmyk");
                smtpClient.Send(mail);


            }


            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();

            var book = _context.Books
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.Category)
                .FirstOrDefault(c => c.Id == id);

            if (book == null) return NotFound();

            // Map the book properties to UpdateBookVM
            var updateBookVM = new UpdateBookVM
            {
                Id = book.Id,
                Name = book.Name,
                AuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList(),
                GenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList(),
                Photo = null, // You may need to handle the image update separately
                CategoryId = book.CategoryId,
                Price = book.Price,
                DiscountPrice = book.DiscountPrice
            };

            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "Name");
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name");
            ViewBag.categories = new SelectList(_context.Categories.ToList(), "Id", "Name");

            return View(updateBookVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(UpdateBookVM updateBookVM)
        {
            if (!ModelState.IsValid) return View(updateBookVM);

            // Retrieve the existing book from the database
            var existingBook = _context.Books
                .Include(b => b.BookGenres)
                .Include(b => b.BookAuthors)
                .FirstOrDefault(c => c.Id == updateBookVM.Id);

            // If the book with the given ID doesn't exist, return a not found response
            if (existingBook == null) return NotFound();

            // Update the book properties with the values from the updateBookVM
            existingBook.Name = updateBookVM.Name;
            existingBook.BookAuthors.Clear();
            existingBook.BookGenres.Clear();

            // Add new book authors based on the selected AuthorIds
            foreach (int authorId in updateBookVM.AuthorIds)
            {
                BookAuthor bookAuthor = new BookAuthor
                {
                    AuthorId = authorId,
                    BookId = existingBook.Id
                };
                existingBook.BookAuthors.Add(bookAuthor);
            }

            // Add new book genres based on the selected GenreIds
            foreach (int genreId in updateBookVM.GenreIds)
            {
                BookGenre bookGenre = new BookGenre
                {
                    GenreId = genreId,
                    BookId = existingBook.Id
                };
                existingBook.BookGenres.Add(bookGenre);
            }

            // Update other book properties
            existingBook.CategoryId = updateBookVM.CategoryId;
            existingBook.Price = updateBookVM.Price;
            existingBook.DiscountPrice = updateBookVM.DiscountPrice;
            existingBook.UpdatedAt = DateTime.Now;

            // Check if a new image is uploaded
            if (updateBookVM.Photo != null)
            {
                string ss = updateBookVM.Photo.SaveImage(webHostEnvironment, "assets/img");
                existingBook.ImgUrl = ss;
            }
            

            // Save the changes to the database
            _context.SaveChanges();

            // Redirect to the index action after updating the book
            return RedirectToAction("Index");
        }
    }
}
