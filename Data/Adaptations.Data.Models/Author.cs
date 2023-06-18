namespace Adaptations.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using Adaptations.Data.Models.Enums;

    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BookGenre Genre { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
