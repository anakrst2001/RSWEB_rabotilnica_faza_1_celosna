using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using MVCBook.Data;

namespace MVCBook.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCBookContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<MVCBookContext>>()))
            {
                // Look for any movies.
                if (context.Book.Any() || context.Author.Any() || context.Genre.Any() || context.Review.Any() || context.UserBook.Any())
                {
                    return; // DB has been seeded
                }

                context.Author.AddRange(
                new Author { /*Id = 1,*/ FirstName = "Fyodor", LastName = "Dostoevsky", BirthDate = DateTime.Parse("1821-11-11"), Nationality="Russian", Gender="male" },
                new Author { /*Id = 2,*/FirstName = "Victor", LastName = "Hugo", BirthDate = DateTime.Parse("1802-02-26"), Nationality = "French", Gender = "male" },
                new Author { /*Id = 3,*/ FirstName = "Oscar", LastName = "Wilde", BirthDate = DateTime.Parse("1854-10-16"), Nationality = "Irish", Gender = "male" },
                new Author { /*Id = 4,*/  FirstName = "Leo", LastName = "Tolstoy", BirthDate = DateTime.Parse("1828-09-20"), Nationality = "Russian", Gender = "male" },
                new Author { /*Id = 5,*/ FirstName = "Franz", LastName = "Kafka", BirthDate = DateTime.Parse("1883-07-03"), Nationality = "Ashkenazi Jewish-Bohemian", Gender = "male" },
                new Author { /*Id = 6,*/ FirstName = "Jane", LastName = "Austen", BirthDate = DateTime.Parse("1775-12-16"), Nationality = "English", Gender = "female" },
                new Author { /*Id = 7,*/ FirstName = "Sylvia", LastName = "Plath", BirthDate = DateTime.Parse("1932-10-27"), Nationality = "American", Gender = "female" }
                );
                context.SaveChanges();
                context.Genre.AddRange(
                new Genre { /*Id = 1, */GenreName="classic" },
                new Genre { /*Id = 2, */GenreName = "fantasy" },
                new Genre { /*Id = 3, */GenreName = "historical fiction" },
                new Genre { /*Id = 4, */GenreName = "horror" },
                new Genre { /*Id = 5, */GenreName = "literary fiction" },
                new Genre { /*Id = 6, */GenreName = "romance" },
                new Genre { /*Id = 7, */GenreName = "science fiction" },
                new Genre { /*Id = 8, */GenreName = "suspense" },
                new Genre { /*Id = 9, */GenreName = "poetry" }
                );
                context.SaveChanges();
                context.Book.AddRange(
                new Book
                {
                    //Id = 1,
                    Title = "Crime and Punishment",
                    YearPublished = 1866,
                    NumPages = 720,
                    Description = "Crime and Punishment follows the mental anguish and moral dilemmas of Rodion Raskolnikov, an impoverished ex-student in Saint Petersburg who plans to kill an unscrupulous pawnbroker, an old woman who stores money and valuable objects in her flat.",
                    Publisher = "Penguin Classics",
                    FrontPage = "Crime_and_punishment.jpg",
                    DownloadUrl = "a",
                    AuthorId = 1
                },
                new Book
                {
                    //Id = 2,
                    Title = "The Bell Jar",
                    YearPublished = 1963,
                    NumPages = 244,
                    Description = "The Bell Jar details the life of Esther Greenwood, a college student who dreams of becoming a poet.",
                    Publisher = "Faber and Faber",
                    FrontPage = "The_bell_jar.jpg",
                    DownloadUrl = "a",
                    AuthorId = 7
                },
                new Book
                {
                    //Id = 3,
                    Title = "The Hunchback of Notre-Dame",
                    YearPublished = 1831,
                    NumPages = 940,
                    Description = "This book tells the story of the beautiful gypsy Esmeralda, condemned as a witch by the tormented archdeacon Claude Frollo, who lusts after her. Quasimodo, the deformed bell ringer of Notre-Dame Cathedral, having fallen in love with the kindhearted Esmeralda, tries to save her by hiding her in the cathedral's tower.",
                    Publisher = "Gosselin",
                    FrontPage = "The_hunchback_of_notre_dame.jpg",
                    DownloadUrl = "a",
                    AuthorId = 2
                },
                new Book
                {
                    //Id = 4,
                    Title = "The picture of Dorian Gray",
                    YearPublished = 1890,
                    NumPages = 288,
                    Description = "This book is an archetypal tale of a young man who purchases eternal youth at the expense of his soul, was a romantic exposition of Wilde’s own Aestheticism.",
                    Publisher = "Penguin Classics",
                    FrontPage = "The_picture_of_dorian_gray.jpg",
                    DownloadUrl = "a",
                    AuthorId = 3
                },
                new Book
                {
                    //Id = 5,
                    Title = "Anna Karenina",
                    YearPublished = 1877,
                    NumPages = 864,
                    Description = "The story centers on an extramarital affair between Anna and dashing cavalry officer Count Alexei Kirillovich Vronsky that scandalizes the social circles of Saint Petersburg and forces the young lovers to flee to Italy in a search for happiness, but after they return to Russia, their lives further unravel.",
                    Publisher = "Simon & Schuster",
                    FrontPage = "Anna_karenina.jpg",
                    DownloadUrl = "a",
                    AuthorId = 4
                },
                new Book
                {
                    //Id = 6,
                    Title = "Emma",
                    YearPublished = 1815,
                    NumPages = 218,
                    Description = "The book centers on the character development of its eponymous protagonist, a genteel young woman on a country estate who meddles in the love lives of friends and neighbors.",
                    Publisher = "John Murray",
                    FrontPage = "Emma.jpg",
                    DownloadUrl = "a",
                    AuthorId = 6
                },
                new Book
                {
                    //Id = 7,
                    Title = "The Metamorphosis",
                    YearPublished = 1915,
                    NumPages = 114,
                    Description = "Metamorphosis tells the story of salesman Gregor Samsa, who wakes one morning to find himself inexplicably transformed into a huge insect and subsequently struggles to adjust to this new condition.",
                    Publisher = "Simon & Schuster",
                    FrontPage = "The_metamorphosis.jpg",
                    DownloadUrl = "a",
                    AuthorId = 5
                },
                new Book
                {
                    //Id = 8,
                    Title = "Ariel",
                    YearPublished = 1965,
                    NumPages = 128,
                    Description = "Ariel by Sylvia Plath describes the terror of a wild horseback ride and the mental and emotional transformation that the rider, and speaker, goes through as she faces death.",
                    Publisher = "Faber and Faber",
                    FrontPage = "Ariel.jpg",
                    DownloadUrl = "a",
                    AuthorId = 7
                }
                );
                context.SaveChanges();
                context.UserBook.AddRange(
                new UserBook { /*Id = 1, */ AppUser="Dave", BookId = 1 },
                new UserBook { /*Id = 2, */ AppUser="Anne", BookId = 1 },
                new UserBook { /*Id = 3, */ AppUser="Alice", BookId = 6 },
                new UserBook { /*Id = 4, */ AppUser="Anne", BookId = 5 },
                new UserBook { /*Id = 5, */ AppUser="Amanda", BookId = 2 }
                );
                context.SaveChanges();
                context.Review.AddRange(
                new Review { /*Id = 1, */ AppUser = "John", Comment = "Awesome book!", Rating = 9, BookId = 1 },
                new Review { /*Id = 2, */ AppUser = "Jack", Comment = "Not my favourite!", Rating = 5, BookId = 6 },
                new Review { /*Id = 3, */ AppUser = "Dave", Comment = "A masterpiece!", Rating = 9, BookId = 2 },
                new Review { /*Id = 4 */ AppUser = "Anne", Comment = "Simply the best!", Rating = 10, BookId = 1 },
                new Review { /*Id = 5, */ AppUser = "Alice", Comment = "I recommend it!", Rating = 7, BookId = 7 },
                new Review { /*Id = 6, */ AppUser = "Matt", Comment = "Lovely!", Rating = 8, BookId = 3 },
                new Review { /*Id = 7, */ AppUser = "Joey", Comment = "My favourite book of all time!", Rating = 10, BookId = 3},
                new Review { /*Id = 7, */ AppUser = "Amanda", Comment = "Kind of dark for my taste!", Rating = 8, BookId = 2 },
                new Review { /*Id = 7, */ AppUser = "Joey", Comment = "I love it!", Rating = 10, BookId = 5 }
                );
                context.SaveChanges();
                context.BookGenre.AddRange
                (
                new BookGenre { GenreId = 1, BookId = 1 },
                new BookGenre { GenreId = 1, BookId = 2 },
                new BookGenre { GenreId = 1, BookId = 3 },
                new BookGenre { GenreId = 1, BookId = 4 },
                new BookGenre { GenreId = 1, BookId = 5 },
                new BookGenre { GenreId = 1, BookId = 6 },
                new BookGenre { GenreId = 1, BookId = 7 },
                new BookGenre { GenreId = 1, BookId = 8 },
                new BookGenre { GenreId = 9, BookId = 8 }
                );
                context.SaveChanges();
            }
        }

    }
}
