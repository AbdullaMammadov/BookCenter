using System.Security.Policy;

namespace FinalProject.ViewModel.Admin.Book
{
    public class CreateBookVM
    {
        public string Name { get; set; }
        public List<int> AuthorIds { get; set; }
        public List<int> GenreIds { get; set; }
        public IFormFile Photo { get; set; }
        public int CategoryId { get; set; }
        public double Price {  get; set; }
        public double? DiscountPrice {  get; set; }
        public string AddedBy {  get; set; }
       
     
    }
}
