using System;
using System.ComponentModel.DataAnnotations;


namespace YellowNotes.Core.Models
{
    public class User
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public DateTime AccountCreationDate { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
