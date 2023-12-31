﻿namespace Adaptations.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using Adaptations.Data.Common.Models;
    using Adaptations.Data.Models.Enums;

    public class Book : BaseDeletableModel<int>
    {
        public Book()
        {
            this.Characters = new HashSet<Character>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(1700, 2023)]
        public int ReleaseYear { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public virtual Movie Movie { get; set; }

        public BookGenre Genre { get; set; }

        public int BooksSold { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

        public bool HasMovie { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public string AddedByUserId { get; set; }
        public virtual ApplicationUser AddedByUser { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
