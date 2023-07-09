namespace Adaptations.Web.ViewModels.Movies
{
    using System.Collections.Generic;

    public class ListAllMovies : PageViewModel
    {
        public IEnumerable<AllMoviesViewModel> Movies { get; set; }
    }
}
