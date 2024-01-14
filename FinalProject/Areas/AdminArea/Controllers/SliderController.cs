using FinalProject.DAL;
using FinalProject.Extension;
using FinalProject.Models;
using FinalProject.ViewModel.Admin.Book;
using FinalProject.ViewModel.Admin.Product;
using FinalProject.ViewModel.Admin.Slider;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace FinalProject.Areas.AdminArea.Controllers
{
    
    [Area("AdminArea")]
    public class SliderController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var sliders = _context.Sliders.ToList(); 

            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateSliderVM createSliderVM)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Photo", "bosh qoyma");
                return View();

            }
            if (createSliderVM.Photo.IsImage())
            {

                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (createSliderVM.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "sekil boyukdur");
                return View();
            }

          

            var newSlider = new Slider
            {
                
                AddedBy = createSliderVM.AddedBy
            };
            newSlider.ImgUrl = createSliderVM.Photo.SaveImage(_webHostEnvironment, "assets/img");
            _context.Sliders.Add(newSlider);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Redirect to the index action or another appropriate action
        }

     
        public async Task<IActionResult> Delete(int id)
        {

           
            var cat = _context.Sliders.FirstOrDefault(c => c.Id == id);
            if (cat == null) return NotFound();
            _context.Sliders.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    

        public IActionResult Update(int id)
        {
            var slider = _context.Sliders.Find(id);

            if (slider == null)
            {
                return NotFound();
            }

            var updateSliderVM = new UpdateSliderVM
            {
                Id = slider.Id,
                AddedBy = slider.AddedBy
            };

            return View(updateSliderVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UpdateSliderVM updateSliderVM)
        {
            if (!ModelState.IsValid)
            {
                return View(updateSliderVM);
            }

            var existingSlider = await _context.Sliders.FindAsync(updateSliderVM.Id);

            if (existingSlider == null)
            {
                return NotFound();
            }

            existingSlider.AddedBy = updateSliderVM.AddedBy;

            // Update the image if a new one is provided
            if (updateSliderVM.Photo != null)
            {
                existingSlider.ImgUrl = updateSliderVM.Photo.FileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Redirect to the index action or another appropriate action
        }
        private string SaveImage(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img"); // Change "uploads" to "img"
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            return uniqueFileName;
        }


    }
}
