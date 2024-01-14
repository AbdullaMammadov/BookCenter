using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.ViewModel.Admin.Slider
{
    public class UpdateSliderVM
    {
    public int Id { get; set; }
    [Required]
  
    public IFormFile Photo { get; set; }
     public string AddedBy { get; set; }

    }
}
