namespace Adaptations.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Adaptations.Common;
    using Adaptations.Data.Models;
    using Adaptations.Services.Data;
    using Adaptations.Web.ViewModels.Movies;
    using Adaptations.Web.ViewModels.Movies.SearchMovie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : BaseController
    {
        private readonly IMoviesService moviesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment environment;

        public MoviesController(
               IMoviesService moviesService,
               UserManager<ApplicationUser> userManager,
               IHostingEnvironment environment
                    )
        {
            this.moviesService = moviesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 9;

            var movies = await this.moviesService.GetAllMoviesAsync<AllMoviesViewModel>(id, ItemsPerPage);


            var movielist = new ListAllMovies
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.moviesService.GetCount(),
                Movies = movies,
            };
            return this.View(movielist);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateMovieInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.moviesService.CreateAsync(model, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, $"Error: {ex.InnerException?.Message ?? ex.Message}");
                return this.View(model);
            }

            this.TempData["Message"] = "Movie added successfully.";

            return this.RedirectToAction("All");
        }

        public IActionResult MovieId(int id)
        {
            var movie = this.moviesService.GetMovieById<SingleMovieViewModel>(id);

            return this.View(movie);
        }

        //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.moviesService.GetMovieById<EditMovieInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditMovieInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.moviesService.EditAsync(id, model);
            return this.RedirectToAction(nameof(this.MovieId), new { id });
        }

        public async Task<IActionResult> Search(string searchResult)
        {
            var viewModel = new ListMoviesViewModel
            {
                SearchText = searchResult,
            };

            var movies = await this.moviesService.GetMoviesBySearchResult<SingleMovieViewModel>(searchResult);

            if (movies == null)
            {
                return this.RedirectToAction(nameof(this.All), "Movies");
            }

            viewModel.Movies = movies;

            return this.View(viewModel);
        }
    }
}
