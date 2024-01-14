using FinalProject.DAL;
using FinalProject.Models;

using FinalProject.ViewModel.Admin.Genre;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
///2
namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class GenreController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GenreController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var genres = _context.Genres.ToList();
            return View(genres);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateGenreVM createGenreVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createGenreVM);
            }

            var newgenre = new Genre
            {
                Name = createGenreVM.Name,
                AddedBy = createGenreVM.AddedBy
            };

            _context.Genres.Add(newgenre);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var genre = _context.Genres.Find(id);

            if (genre == null)
            {
                return NotFound();
            }

            var updateGenreVM = new UpdateGenreVM
            {
                Id = genre.Id,
                Name = genre.Name,
                AddedBy = genre.AddedBy
            };

            return View(updateGenreVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UpdateGenreVM updategenreVM)
        {
            if (!ModelState.IsValid)
            {
                return View(updategenreVM);
            }

            var existinggenre = await _context.Genres.FindAsync(updategenreVM.Id);

            if (existinggenre == null)
            {
                return NotFound();
            }

            existinggenre.Name = updategenreVM.Name;
            existinggenre.AddedBy = updategenreVM.AddedBy;
            existinggenre.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var cat = _context.Genres.FirstOrDefault(c => c.Id == id);
            if (cat == null) return NotFound();
            _context.Genres.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
