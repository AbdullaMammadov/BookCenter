using FinalProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.ViewModel.Admin.Book
{

   
        public class UpdateBookVM
        {
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            public List<int> AuthorIds { get; set; }
            public List<int> GenreIds { get; set; }
            public IFormFile Photo { get; set; }
            public int CategoryId { get; set; }
            public double Price { get; set; }
            public double? DiscountPrice { get; set; }
        }
    

}