using FinalProject.Models;

namespace FinalProject.ViewModel
{
    public class BookVM
    {
        public Book? Book { get; set; }
        public Product? Prodcuct { get; set; }
        public List<Book>? Books { get; set; }
        public List<Product>? Products { get; set; }
        public string CategoryName { get; set; }
        public List<string>? AuthorNames {  get; set; }
        public List<string>? GenreNames { get; set; }
    }
}
