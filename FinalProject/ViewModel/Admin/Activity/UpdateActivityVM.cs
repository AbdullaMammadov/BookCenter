
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel.Admin.Activity
{
    public class UpdateActivityVM
    {

        public string Title { get; set; }
        public int Id { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        public string Description { get; set; }
        public string? Desc2 { get; set; }
        public string? Desc3 { get; set; }
        public string AddedBy { get; set; }
    }
}
