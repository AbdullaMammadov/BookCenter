namespace FinalProject.Models
{
    public class Product:BaseEntity
    {

        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public int CategoryProId { get; set; }
        public CategoryPro CategoryPro { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }

        public List<SaleProduct> SaleProducts { get; set; }
    }
}
