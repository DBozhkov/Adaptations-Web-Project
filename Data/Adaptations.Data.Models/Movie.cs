using Adaptations.Data.Common.Models;
using Adaptations.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string MovieName { get; set; }

        public int ReleaseYear { get; set; }

        public string MoviePlot { get; set; }

        public double Rating { get; set; }

        public MovieGenre Genre { get; set; }

        public string DirectorName { get; set; }

        public int RunTime { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        [ForeignKey(nameof(Book))]
        public int? BookId { get; set; }
        public virtual Book Book { get; set; }

        public virtual ICollection<ActorMovie> ActorsMovies { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
