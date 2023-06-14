using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adaptations.Data.Models
{
    public class AuthorBook
    {
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }
        public Author Author { get; set; }

        [ForeignKey(nameof(Book))]
        public string BookId { get; set; }
        public Book Book { get; set; }

    }
}
