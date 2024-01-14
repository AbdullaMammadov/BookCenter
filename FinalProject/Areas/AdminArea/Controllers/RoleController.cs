using FinalProject.Models;
using FinalProject.ViewModels.Admin.RoleVM;
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }


        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null) NotFound();

            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

    }
}