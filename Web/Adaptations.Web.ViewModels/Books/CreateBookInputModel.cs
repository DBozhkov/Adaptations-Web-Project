namespace Adaptations.Web.ViewModels.Books
{
    using System.Collections.Generic;

    using Adaptations.Data.Models.Enums;
    using Microsoft.AspNetCore.Http;

    public class CreateBookInputModel
    {
        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public string Description { get; set; }

        public BookGenre Genre { get; set; }

        public string AuthorName { get; set; }

        public string AuthorBiography { get; set; }

        public int BooksSold { get; set; }

        public IEnumerable<CharacterInputModel> Characters { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
