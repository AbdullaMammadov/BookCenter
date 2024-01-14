namespace FinalProject.Models
{
    public class SaleBook
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public double Price { get; set; }

    }
}
