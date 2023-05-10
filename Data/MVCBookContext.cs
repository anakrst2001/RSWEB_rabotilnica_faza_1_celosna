using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCBook.Models;

namespace MVCBook.Data
{
    public class MVCBookContext : DbContext
    {
        public MVCBookContext (DbContextOptions<MVCBookContext> options)
            : base(options)
        {
        }

        public DbSet<MVCBook.Models.Book> Book { get; set; } = default!;

        public DbSet<MVCBook.Models.Author>? Author { get; set; }

        public DbSet<MVCBook.Models.Genre>? Genre { get; set; }

        public DbSet<MVCBook.Models.Review>? Review { get; set; }

        public DbSet<MVCBook.Models.UserBook>? UserBook { get; set; }

        public DbSet<BookGenre> BookGenre { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookGenre>()
            .HasOne<Book>(p => p.Book)
            .WithMany(p => p.Genres)
            .HasForeignKey(p => p.BookId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<BookGenre>()
            .HasOne<Genre>(p => p.Genre)
            .WithMany(p => p.Books)
            .HasForeignKey(p => p.GenreId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Book>()
            .HasOne<Author>(p => p.Author)
            .WithMany(p => p.Books)
            .HasForeignKey(p => p.AuthorId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Review>()
            .HasOne<Book>(p => p.Book)
            .WithMany(p => p.Reviews)
            .HasForeignKey(p => p.BookId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<UserBook>()
            .HasOne<Book>(p => p.Book)
            .WithMany(p => p.UserBooks)
            .HasForeignKey(p => p.BookId);
            //.HasPrincipalKey(p => p.Id);
        }
    }
}
