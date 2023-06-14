using Adaptations.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class Movie
    {
        public Movie()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string MovieName { get; set; }

        public int ReleaseYear { get; set; }

        public double Rating { get; set; }

        public MovieGenre Genre { get; set; }

        [ForeignKey(nameof(Image))]
        public string ImageId { get; set; }
        public Image Image { get; set; }

        public ICollection<ActorMovie> ActorsMovies { get; set; }
    }
}
