using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace MVCBook.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Year Published")]
        public int? YearPublished { get; set; }

        [Display(Name ="Number of pages")]
        public int? NumPages { get; set; }

        public string? Description { get; set; }
        [StringLength(50)]
        public string? Publisher { get; set; }
        [Display(Name ="Front Page")]
        public string? FrontPage { get; set; }

        [Display(Name ="Download URL")]
        public string? DownloadUrl { get; set; }
       
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public ICollection<BookGenre>? Genres { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<UserBook>? UserBooks { get; set; }
    }
}
