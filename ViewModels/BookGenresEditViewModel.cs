using Microsoft.AspNetCore.Mvc.Rendering;
using MVCBook.Models;
using System.Collections.Generic;

namespace MVCBook.ViewModels
{
    public class BookGenresEditViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }

    }
}
