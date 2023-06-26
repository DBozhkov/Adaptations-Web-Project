using System;
using System.Collections.Generic;
using System.Text;

namespace Adaptations.Web.ViewModels
{
    public class PageViewModel
    {
        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.RecipesCount / this.ItemsPerPage);

        public int RecipesCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
