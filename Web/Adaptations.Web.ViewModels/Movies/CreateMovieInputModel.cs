namespace Adaptations.Web.ViewModels.Movies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Adaptations.Data.Models.Enums;
    using Microsoft.AspNetCore.Http;

    public class CreateMovieInputModel
    {
        [Required]
        [Display(Name = "Movie Name")]
        public string MovieName { get; set; }

        [Required]
        [Display(Name = "Plot")]
        public string MoviePlot { get; set; }

        [Required]
        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }

        [Required]
        [Display(Name = "Runtime")]
        public int RunTime { get; set; }

        [Required]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [Required]
        public double Rating { get; set; }

        public MovieGenre Genre { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
