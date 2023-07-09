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
        int GetCount();
        Task<IEnumerable<T>> GetRandom<T>(int count);
        Task<IEnumerable<T>> GetMoviesBySearchResult<T>(string searchResult);
        Task DeleteByIdAsync(int? id);
        Task EditAsync(int? id, EditMovieInputModel model);
        Task<IEnumerable<T>> GetAllBooksByMovieId<T>(int id);
        Task<IEnumerable<T>> GetAllActorsByMovieId<T>(int id);
    }
}
