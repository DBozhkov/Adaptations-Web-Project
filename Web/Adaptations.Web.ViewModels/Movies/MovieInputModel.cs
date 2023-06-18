namespace Adaptations.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Adaptations.Data.Models.Enums;

    public class MovieInputModel
    {
        [MinLength(3)]
        [Display(Name = "Movie Name")]
        public string MovieName { get; set; }

        [Display(Name = "Movie Plot")]
        public string MoviePlot { get; set; }

        public MovieGenre Genre { get; set; }
    }
}
