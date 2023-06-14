using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public byte[] ImageData { get; set; }

        [ForeignKey(nameof(Actor))]
        public string ActorId { get; set; }
        public Actor Actor { get; set; }

        [ForeignKey(nameof(Movie))]
        public string MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey(nameof(Book))]
        public string BookId { get; set; }
        public Book Book { get; set; }
    }
}
