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

        [Required]
        [MaxLength(100)]
        public string MovieName { get; set; }

        [Required]
        [Range(1900, 2023)]
        public int ReleaseYear { get; set; }

        [Required]
        [MaxLength(1000)]
        public string MoviePlot { get; set; }

        [Required]
        [Range(0, 10)]
        public double Rating { get; set; }

        public MovieGenre Genre { get; set; }

        public string DirectorName { get; set; }

        [Required]
        [Range(0, 500)]
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
