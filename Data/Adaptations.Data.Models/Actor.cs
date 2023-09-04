namespace Adaptations.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Biography { get; set; }

        public string Country { get; set; }

        public DateTime BornOn { get; set; }

        public DateTime DiedOn { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ActorMovie> ActorsMovies { get; set; }
    }
}
