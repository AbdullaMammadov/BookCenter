using FinalProject.DAL;
using FinalProject.Models;

using FinalProject.ViewModel.Admin.Author;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
///2
namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class AuthorController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuthorController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateAuthorVM createAuthorVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createAuthorVM);
            }

            var newAuthor = new Author
            {
                Name = createAuthorVM.Name,
                AddedBy = createAuthorVM.AddedBy
            };

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var Author = _context.Authors.Find(id);

            if (Author == null)
            {
                return NotFound();
            }

            var updateAuthorVM = new UpdateAuthorVM
            {
                Id = Author.Id,
                Name = Author.Name,
                AddedBy = Author.AddedBy
            };

            return View(updateAuthorVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UpdateAuthorVM updateAuthorVM)
        {
            if (!ModelState.IsValid)
            {
                return View(updateAuthorVM);
            }

            var existingAuthor = await _context.Authors.FindAsync(updateAuthorVM.Id);

            if (existingAuthor == null)
            {
                return NotFound();
            }

            existingAuthor.Name = updateAuthorVM.Name;
            existingAuthor.AddedBy = updateAuthorVM.AddedBy;
            existingAuthor.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var cat = _context.Authors.FirstOrDefault(c => c.Id == id);
            if (cat == null) return NotFound();
            _context.Authors.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
