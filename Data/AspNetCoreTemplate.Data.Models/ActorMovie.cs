using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class ActorMovie
    {
        [ForeignKey(nameof(Actor))]
        public string ActorId { get; set; }
        public Actor Actor { get; set; }

        [ForeignKey(nameof(Movie))]
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
