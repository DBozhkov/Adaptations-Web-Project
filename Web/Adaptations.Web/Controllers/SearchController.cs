namespace Adaptations.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Adaptations.Services.Data;
    using Adaptations.Web.ViewModels.Books;
    using Adaptations.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        private const int ItemsPerPage = 9;
        private readonly IMoviesService moviesService;
        private readonly IBooksService booksService;

        public SearchController(IMoviesService moviesService, IBooksService booksService)
        {
            this.moviesService = moviesService;
            this.booksService = booksService;
        }

        public IActionResult GetResult(string searchInput, int? id = 1)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            string input = searchInput ?? string.Empty;

            if (this.moviesService.IsSearchResultMovie(input))
            {
                return this.RedirectToAction("SearchByMovieName", new { id, input = searchInput });
            }

            if (this.booksService.IsSearchResultBook(input))
            {
                return this.RedirectToAction("SearchByBookTitle", new { id, input = searchInput });
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> SearchByMovieName(string input, int id)
        {
            var movies = await this.moviesService.GetMoviesBySearchResult<AllMoviesViewModel>(input, id, ItemsPerPage);

            var searchMovieList = new ListAllMovies
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = movies.Count(),
                Movies = movies,
            };
            return this.View(searchMovieList);
        }

        public async Task<IActionResult> SearchByBookTitle(string input, int id)
        {
            var books = await this.moviesService.GetMoviesBySearchResult<AllBooksViewModel>(input, id, ItemsPerPage);

            var searchBooksList = new ListAllBooks
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = books.Count(),
                Books = books,
            };
            return this.View(searchBooksList);
        }
    }
}
