using FinalProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.ViewModel.Admin.Category
{


    public class UpdateCategoryVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
  
         public string AddedBy { get; set; }
    }


}