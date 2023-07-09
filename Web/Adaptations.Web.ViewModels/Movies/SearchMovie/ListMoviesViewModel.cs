using Adaptations.Data.Models;
using Adaptations.Services.Mapping;
using System.Collections.Generic;

namespace Adaptations.Web.ViewModels.Movies.SearchMovie
{
    public class ListMoviesViewModel : IMapFrom<Movie>
    {
        public IEnumerable<SingleMovieViewModel> Movies { get; set; }

        public string SearchText { get; set; }
    }
}
