
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel.Account
{
    public class RegisterVM
    {
        [Required, StringLength(100)]

        public string FirstName { get; set; }
        [Required, StringLength(100)]

        public string LastName { get; set; }
        [Required, StringLength(100)]

        public string Username { get; set; }
        [Required, EmailAddress, DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]

        public string RepeatPassword { get; set; }
    }
}