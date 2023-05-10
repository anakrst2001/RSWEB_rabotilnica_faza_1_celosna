using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCBook.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="App User")]
        [StringLength(450)]
        public string AppUser { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }
        public int? Rating { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
