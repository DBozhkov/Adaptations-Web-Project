using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class ActorMovie
    {
        [ForeignKey(nameof(Actor))]
        public int ActorId { get; set; }
        public virtual Actor Actor { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
