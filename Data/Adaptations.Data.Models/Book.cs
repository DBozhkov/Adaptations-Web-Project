using Adaptations.Data.Common.Models;
using Adaptations.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class Book : BaseDeletableModel<int>
    {
        public Book()
        {
            this.Characters = new HashSet<Character>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        public BookGenre Genre { get; set; }

        public int BooksSold { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

        public bool HasMovie { get; set; }
        
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
