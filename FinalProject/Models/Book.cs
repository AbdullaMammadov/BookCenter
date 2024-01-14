using FinalProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }
        
        public string ImgUrl {  get; set; }
        public List<BookGenre> BookGenres { get; set; }
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
        public double Price {  get; set; }
        public double? DiscountPrice { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
        public List<SaleBook> SaleBook { get; set; }

        public Book()
        {
           
            BookAuthors = new List<BookAuthor>();
            BookGenres = new List<BookGenre>();
        }
    }
}
