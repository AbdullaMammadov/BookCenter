namespace FinalProject.Models
{
    public class CategoryPro:BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
