namespace FinalProject.Models
{
    public class Genre:BaseEntity
    {
      
        public string Name { get; set; }

        public List<BookGenre> BookGenres { get; set; }
    }
}
