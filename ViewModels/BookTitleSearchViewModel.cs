using Microsoft.AspNetCore.Mvc.Rendering;
using MVCBook.Models;
using System.Collections.Generic;

namespace MVCBook.ViewModels
{
    public class BookTitleSearchViewModel
    {
        public IList<Book> Books { get; set; }
        public string SearchString { get; set; }
    }
}
