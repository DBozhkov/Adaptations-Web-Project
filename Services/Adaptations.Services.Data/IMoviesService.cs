using Adaptations.Web.ViewModels.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adaptations.Services.Data
{
    public interface IMoviesService
    {
        Task CreateAsync(CreateMovieInputModel inputMovie, string userId, string imagePath);

        T GetMovieById<T>(int id);

        Task<IEnumerable<T>> GetAllMoviesAsync<T>(int page, int itemsPerPage = 9);

        Task<IEnumerable<T>> SortByNameAsync<T>(int page, int itemsPerPage = 9);

        Task<IEnumerable<T>> SortByAddedAsync<T>(int page, int itemsPerPage = 9);

        int GetBookId(int id);

        int GetCount();

        Task<Dictionary<int, string>> GetRandom();

        Task<IEnumerable<T>> GetMoviesBySearchResultAsync<T>(string searchResult, int page, int itemsPerPage = 9);

        Task DeleteByIdAsync(int? id);

        Task EditAsync(int? id, EditMovieInputModel model);

        Task<IEnumerable<T>> GetAllBooksByMovieId<T>(int id);

        Task<IEnumerable<T>> GetAllActorsByMovieId<T>(int id);

        bool IsSearchResultMovie(string input);
    }
}
