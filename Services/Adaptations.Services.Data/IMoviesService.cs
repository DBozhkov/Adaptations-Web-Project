using Adaptations.Web.ViewModels.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adaptations.Services.Data
{
    public interface IMoviesService
    {
        Task CreateAsync(CreateMovieInputModel inputMovie, string userId/*, string imagePath*/);
        Task<MovieViewModel> GetMovieByIdAsync(int id);
        Task<IEnumerable<AllMoviesViewModel>> GetAllMoviesAsync();
        Task<IEnumerable<T>> GetRandom<T>(int count);
        Task<IEnumerable<MovieViewModel>> GetMoviesBySearchResult(string searchResult, int? movieId);
        Task DeleteByIdAsync(int id);
        Task EditAsync(MovieViewModel model);
        Task<IEnumerable<MovieViewModel>> GetAllMoviesByBook();
        Task<IEnumerable<MovieViewModel>> GetAllMoviesByActor();
    }
}
