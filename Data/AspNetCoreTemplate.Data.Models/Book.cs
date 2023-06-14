using Adaptations.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class Book
    {
        public Book()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Characters = new HashSet<Character>();
            this.AuthorsBooks = new HashSet<AuthorBook>();
        }
        public string Id { get; set; }

        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(Movie))]
        public string MovieId { get; set; }
        public Movie Movie { get; set; }

        public BookGenre Genre { get; set; }

        public int BooksSold { get; set; }

        public ICollection<Character> Characters { get; set; }

        public bool IsDeleted { get; set; }

        public bool HasMovie { get; set; }

        public ICollection<AuthorBook> AuthorsBooks { get; set; }

    }
}
