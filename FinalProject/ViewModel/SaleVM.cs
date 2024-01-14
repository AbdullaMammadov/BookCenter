using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel
{
    public class SaleVM
    {
        [Required(ErrorMessage = "Name and Surname is required", AllowEmptyStrings = true)]
        public string NameAndSurname { get; set; }

        [Required(ErrorMessage = "Email is required", AllowEmptyStrings = true)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required", AllowEmptyStrings = true)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Card Holder's Name and Surname is required")]
        public string CardNameAndSurname { get; set; }

        [Required(ErrorMessage = "Card Number is required")]
        [CreditCard(ErrorMessage = "Invalid Credit Card Number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiration Month is required")]
        public string ExpMonth { get; set; }

        [Required(ErrorMessage = "Expiration Year is required")]
        [Range(1970, 2100, ErrorMessage = "Please enter a valid year")]
        public int ExpYear { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        public int Cvv { get; set; }
    }
}
