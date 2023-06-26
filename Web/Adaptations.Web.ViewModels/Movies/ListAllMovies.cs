using System;
using System.Collections.Generic;
using System.Text;

namespace Adaptations.Web.ViewModels.Movies
{
    public class ListAllMovies : PageViewModel
    {
        public IEnumerable<AllMoviesViewModel> Movies { get; set; }
    }
}
