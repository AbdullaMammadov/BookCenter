using FinalProject.Helpers;
using FinalProject.Models;

namespace FinalProject.ViewModel
{
    public class ShopVM
    {

        public List<Product>? Products { get; set; }

        public List<Book>? Books { get; set; }

        public Pagination<Product>? paginationP { get; set; }
        public Pagination<Book>? paginationB { get; set; }



    }
}
