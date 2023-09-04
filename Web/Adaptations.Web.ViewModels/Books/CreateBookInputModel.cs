namespace Adaptations.Web.ViewModels.Books
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Adaptations.Data.Models.Enums;
    using Microsoft.AspNetCore.Http;

    public class CreateBookInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [Range(1700, 2023)]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(1000)]
        public string Description { get; set; }

        public BookGenre Genre { get; set; }

        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }

        [Display(Name = "Author Bio")]
        public string AuthorBiography { get; set; }

        [Display(Name = "Books Sold:")]
        public int BooksSold { get; set; }

        public IEnumerable<CharacterInputModel> Characters { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public string MovieName { get; set; }
    }
}
