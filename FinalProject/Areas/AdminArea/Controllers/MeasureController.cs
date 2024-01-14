using Microsoft.AspNetCore.Mvc;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModel.Admin.Activity;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using FinalProject.Extension;
using Microsoft.AspNetCore.Hosting;
using FinalProject.ViewModel.Admin.Book;

namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class MeasureController : Controller
    {
        private readonly AppDbContext _context;

        private IWebHostEnvironment webHostEnvironment;

        public MeasureController(AppDbContext context, IWebHostEnvironment weBHostEnvironment)
        {
            _context = context;
            webHostEnvironment = weBHostEnvironment;
        }

        // Index Action
        public IActionResult Project()
        {
            var activities = _context.Activities.Where(a => a.IsTedbir == false).ToList();
            return View(activities);
        }
        public IActionResult Measure()
        {
            var activities = _context.Activities.Where(a => a.IsTedbir == true).ToList();
            return View(activities);
        }

        // Create Action
        public IActionResult Create()
        {
            return View();
        }

        // Handle the form submission for creating an activity
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateActivityVM createActivity)
        {
            if (!ModelState.IsValid)
            {
                return View(createActivity);
            }

            var imgUrl = createActivity.Photo.FileName;

            var newActivity = new Activity
            {
                Title = createActivity.Title,
                ImgUrl = imgUrl,
                Description = createActivity.Description,
                Desc2 = createActivity.Desc2,
                Desc3 = createActivity.Desc3,
                AddedBy = createActivity.AddedBy,
                CreatedAt=DateTime.Now,
                Date = DateTime.Now,
                IsTedbir = createActivity.IsTedbir// Set IsTedbir to true
            };

            _context.Activities.Add(newActivity);
            await _context.SaveChangesAsync();

            return RedirectToAction("Measure");
        }

        // Update Action
        public IActionResult Update(int id)
        {
            var activity = _context.Activities.Find(id);

            if (activity == null)
            {
                return NotFound();
            }

            var updateActivityVM = new UpdateActivityVM
            {
                Id = activity.Id,
                Title = activity.Title,
                Description = activity.Description,
                Desc2 = activity.Desc2,
                Desc3 = activity.Desc3, 
               
                AddedBy = activity.AddedBy
            };

            return View(updateActivityVM);
        }

        // Handle the form submission for updating an activity
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int id, UpdateActivityVM updateActivity)
        {
            if (!ModelState.IsValid)
            {
                return View(updateActivity);
            }

            var existingActivity = await _context.Activities.FindAsync(id);

            if (existingActivity == null)
            {
                return NotFound();
            }

            existingActivity.Title = updateActivity.Title;
            existingActivity.Description = updateActivity.Description;
            existingActivity.Desc2 = updateActivity.Desc2;
            existingActivity.Desc3 = updateActivity.Desc3;
            existingActivity.AddedBy = updateActivity.AddedBy;
            existingActivity.UpdatedAt= DateTime.Now;

            // Update the image if a new one is provided
            if (updateActivity.Photo != null)
            {
                existingActivity.ImgUrl = updateActivity.Photo.SaveImage(webHostEnvironment, "assets/img");
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Measure");
        }

        // Delete Action
        public async Task<IActionResult> Delete(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return RedirectToAction("measure");
        }

        public IActionResult UploadImage(IFormFile imageFile)
        {
            // Check if the uploaded file is an image
            if (imageFile.IsImage())
            {
                ModelState.AddModelError("imageFile", "Only image files are allowed.");
                return View();
            }

            // Check if the image size exceeds the specified limit (in kilobytes)
            if (imageFile.CheckImageSize(1000))
            {
                ModelState.AddModelError("imageFile", "Image size should be less than 1000 KB.");
                return View();
            }

            // Save the image to the specified folder
            string folderPath = "your_folder_name"; // Replace with your desired folder name
            string imagePath = imageFile.SaveImage(webHostEnvironment, folderPath);

            // You can use imagePath for further processing or storing in your database

            return RedirectToAction("Index");
        }
    }
}
