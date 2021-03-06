using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YellowNotes.Core.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        public int UserId { get; set; }

        public int? CategoryId { get; set; }

        [Required]
        public DateTime ModificationDate { get; set; }

        [Required]
        public NoteVariant Variant { get; set; }

        [Required]
        public bool IsRemoved { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Notes")]
        public virtual User User { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Notes")]
        public virtual Category Category { get; set; }
    }
}
