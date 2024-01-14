﻿using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModel.Admin.Book;
using FinalProject.ViewModel.Admin.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
///2
namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVM createCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createCategoryVM);
            }

            var newCategory = new Category
            {
                Name = createCategoryVM.Name,
                AddedBy = createCategoryVM.AddedBy,
                CreatedAt = DateTime.Now
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            var updateCategoryVM = new UpdateCategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                AddedBy = category.AddedBy
                
            };

            return View(updateCategoryVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UpdateCategoryVM updateCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(updateCategoryVM);
            }

            var existingCategory = await _context.Categories.FindAsync(updateCategoryVM.Id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = updateCategoryVM.Name;
            existingCategory.AddedBy = updateCategoryVM.AddedBy;
            existingCategory.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); 
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var cat= _context.Categories.FirstOrDefault(c => c.Id == id);
            if (cat == null) return NotFound();
            _context.Categories.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
