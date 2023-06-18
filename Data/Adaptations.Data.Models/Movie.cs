using Adaptations.Data.Common.Models;
using Adaptations.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.Images = new HashSet<Image>();
            this.ActorsMovies = new HashSet<ActorMovie>();
        }

        public int MovieId { get; set; }

        public string MovieName { get; set; }

        public int ReleaseYear { get; set; }

        public double Rating { get; set; }

        public MovieGenre Genre { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ActorMovie> ActorsMovies { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
