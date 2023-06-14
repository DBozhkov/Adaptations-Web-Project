using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class Actor
    {
        public Actor()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime BornOn { get; set; }

        public bool IsAlive { get; set; }

        public DateTime DiedOn { get; set; }

        [ForeignKey(nameof(Image))]
        public string ImageId { get; set; }
        public Image Image { get; set; }

        [ForeignKey(nameof(Character))]
        public string CharacterId { get; set; }
        public Character Character { get; set; }

        public ICollection<ActorMovie> ActorsMovies { get; set; }
    }
}
