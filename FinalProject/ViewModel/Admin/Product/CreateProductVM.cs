using System.Security.Policy;

namespace FinalProject.ViewModel.Admin.Product
{
    public class CreateProductVM
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public string AddedBy { get; set; }

    }
}
