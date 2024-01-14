using FinalProject.Models;
using Humanizer.DateTimeHumanizeStrategy;

namespace FinalProject.Helpers
{
    public class Pagination<T>
    {
        public List<T> Items { get; set; }
        public List<Product>? Products { get; set; }
        public List<Book>? Books { get; set; }
       public int PageCount { get; set; }

        public int CurrentPage { get; set; }


        public Pagination(List<T> items, int pageCount, int currentPage, List<Product>? products, List<Book>? books)
        {
            this.Items = items;
            this.PageCount = pageCount;
            this.CurrentPage = currentPage;
            this.Products = products;
            this.Books = books;

        }


    }
}