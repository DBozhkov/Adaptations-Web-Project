using Adaptations.Services.Data;
using Adaptations.Web.ViewModels.Movies.SearchMovie;
using Microsoft.AspNetCore.Mvc;

namespace Adaptations.Web.Controllers
{
    public class SearchMovieController : BaseController
    {
        private readonly IMoviesService moviesService;

        public SearchMovieController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        //public IActionResult Index()
        //{
        //    var viewModel = new ListMoviesViewModel
        //    {
        //        Movies = this.moviesService.GetAllMoviesAsync<SingleMovieViewModel>(),
        //    };
        //    return this.View(viewModel);
        //}

        //[HttpGet]
        //public IActionResult MovieList(SearchListInputModel input)
        //{
        //    var viewModel = new ListViewModel
        //    {
        //        Recipes = this.recipesService
        //        .GetByIngredients<RecipeInListViewModel>(input.Ingredients),
        //    };

        //    return this.View(viewModel);
        //}
    }
}
