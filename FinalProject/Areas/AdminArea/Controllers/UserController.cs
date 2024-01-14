using FinalProject.Models;
using FinalProject.ViewModels.Admin.RoleVM;
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAL;

namespace FinalProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index(string search)
        {

            var users = search == null ? _userManager.Users.ToList() :
                  _userManager.Users
                  .Where(u => u.UserName.ToLower().Contains(search.ToLower())).ToList();



            return View(users);


        }

        public async Task<IActionResult> ChangeStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsActive = !user.IsActive;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("index");
        }



        public async Task<IActionResult> Update(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            UpdateRoleVM updateRoleVM = new UpdateRoleVM
            {
                Roles = _roleManager.Roles.ToList(),
                UserRoles = await _userManager.GetRolesAsync(user),
                User = user

            };
            return View(updateRoleVM);
        }

        [HttpPost]

        public async Task<IActionResult> Update(string id, List<string> roles)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var userOldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userOldRoles);


            await _userManager.AddToRolesAsync(user, roles);
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle errors, you may want to log them or display to the user
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(); // or return an error view
            }
        }






    }
}