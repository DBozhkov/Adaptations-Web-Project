using Adaptations.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string ImageId { get; set; }

        public byte[] ImageData { get; set; }

        public string Extension { get; set; }

        [ForeignKey(nameof(Actor))]
        public int ActorId { get; set; }
        public virtual Actor Actor { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
