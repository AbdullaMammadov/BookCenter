using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.ViewModel.Admin.Product
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        

    }
}
