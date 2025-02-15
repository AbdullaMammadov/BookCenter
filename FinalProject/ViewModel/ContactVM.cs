﻿using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModel
{
    public class ContactVM
    {
        [Required(ErrorMessage = "Subject is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }
    }
}