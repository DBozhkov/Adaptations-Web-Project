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
        [MinLength(2)]
        [MaxLength(100)]
        public string MovieName { get; set; }

        [Required]
        [Display(Name = "Plot")]
        [MinLength(10)]
        [MaxLength(1000)]
        public string MoviePlot { get; set; }

        [Required]
        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }

        [Required]
        [Display(Name = "Runtime")]
        [Range(0, 500)]
        public int RunTime { get; set; }

        [Required]
        [Display(Name = "Release Year")]
        [Range(1900, 2023)]
        public int ReleaseYear { get; set; }

        [Required]
        [Range(0, 10)]
        public double Rating { get; set; }

        public MovieGenre Genre { get; set; }

        public IEnumerable<ActorInputModel> Actors { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public string BookTitle { get; set; }
    }
}
