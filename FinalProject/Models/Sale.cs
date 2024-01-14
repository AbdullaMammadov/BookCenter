using FinalProject.Models;

namespace FinalProject.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<SaleProduct>? SaleProducts { get; set; }
        public List<SaleBook>? SaleBooks { get; set; }
        public Sale()
        {
            SaleProducts = new List<SaleProduct>();
            SaleBooks = new List<SaleBook>();

        }


        public string NameAndSurname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CardNameAndSurname { get; set; }
        public string CardNumber { get; set; }
        public string ExpMonth { get; set; }
        public int ExpYEAR { get; set; }
        public int Cvv { get; set; }


    }
}
