using FinalProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.ViewModel.Admin.Author
{


    public class UpdateAuthorVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string AddedBy { get; set; }
    }


}