using System;
using System.ComponentModel.DataAnnotations;

namespace YellowNotes.Core.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
