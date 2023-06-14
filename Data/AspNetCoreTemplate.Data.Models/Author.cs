using Adaptations.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adaptations.Data.Models
{
    public class Author
    {
        public Author()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public BookGenre Genre { get; set; }

        public ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}
