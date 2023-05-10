using Microsoft.AspNetCore.Mvc.Rendering;
using MVCBook.Models;
using System.Collections.Generic;

namespace MVCBook.ViewModels
{
    public class AuthorNameSurnameNationality
    {
        public IList<Author> Authors { get; set; }
        public SelectList Nationalities { get; set; }
        public string AuthorNationality { get; set; }
        public string SearchStringName { get; set; }

        public string SearchStringSurname { get; set; }

    }
}
