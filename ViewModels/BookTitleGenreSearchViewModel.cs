using Microsoft.AspNetCore.Mvc.Rendering;
using MVCBook.Models;
using System.Collections.Generic;

namespace MVCBook.ViewModels
{
    public class BookTitleGenreSearchViewModel
    {
        public IList<Book> Books { get; set; }
        public SelectList Genres { get; set; }
        public string BookGenre { get; set; }
        public string SearchString { get; set; }
    }
}
