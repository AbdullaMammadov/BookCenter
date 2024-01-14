
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.ViewModels.Admin.RoleVM
{
    public class UpdateRoleVM
    {
        public List<IdentityRole> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
        public AppUser User { get; set; }
    }
}