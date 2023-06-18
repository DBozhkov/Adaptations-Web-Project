using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BornOn { get; set; }

        public bool IsAlive { get; set; }

        public DateTime DiedOn { get; set; }

        [ForeignKey(nameof(Image))]
        public string ImageId { get; set; }
        public virtual Image Image { get; set; }

        [ForeignKey(nameof(Character))]
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }

        public virtual ICollection<ActorMovie> ActorsMovies { get; set; }
    }
}
