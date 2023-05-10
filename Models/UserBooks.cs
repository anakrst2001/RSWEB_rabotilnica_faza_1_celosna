using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCBook.Models
{
    public class UserBooks
    {
        public int Id { get; set; }

        [Display(Name ="App User")]
        [StringLength(450)]
        public string AppUser { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
