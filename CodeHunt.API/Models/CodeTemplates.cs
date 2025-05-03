using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeHunt.API.Models
{
    public class CodeTemplate
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ExerciseId { get; set; }

        [Required]
        public Guid LanguageId { get; set; }

        [Required]
        public required string Template { get; set; }

        [ForeignKey("LanguageId")]
        public Language? Language { get; set; }
    }
}
