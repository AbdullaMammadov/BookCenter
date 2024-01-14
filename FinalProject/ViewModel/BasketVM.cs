namespace FinalProject.ViewModels
{
    public class BasketVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int BasketCount { get; set; }
        public string ImageUrl { get; set; }
        public string ProductType { get; set; } 
        public bool? IsBook { get; set; }
        public int? CategoryProId { get; set; }
        public double? DiscountPrice { get; set; }
    }
}