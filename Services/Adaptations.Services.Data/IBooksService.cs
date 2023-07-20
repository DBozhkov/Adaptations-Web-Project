using Adaptations.Web.ViewModels.Books;
using Adaptations.Web.ViewModels.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adaptations.Services.Data
{
    public interface IBooksService
    {
        Task CreateAsync(CreateBookInputModel inputMovie, string userId, string imagePath);
        T GetBookAsync<T>(int id);
        Task<IEnumerable<T>> GetAllBooksAsync<T>(int page, int itemsPerPage = 9);
        int GetCount();
        Task<IEnumerable<T>> GetRandom<T>(int count);
        Task<IEnumerable<T>> GetBooksBySearchResult<T>(string searchResult, int page, int itemsPerPage = 9);
        Task DeleteByIdAsync(int id);
        Task EditAsync(int? id, EditMovieInputModel model);
        Task<IEnumerable<T>> GetAllMoviesByBookId<T>(int id);
        bool IsSearchResultBook(string input);
    }
}
