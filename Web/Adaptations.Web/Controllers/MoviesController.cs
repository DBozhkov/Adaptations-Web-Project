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
        private const int ItemsPerPage = 9;
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

            if (movie.BookId == 0)
            {
                var bookId = this.moviesService.GetBookId(id);

                movie.BookId = bookId;
            }

            return this.View(movie);
        }

        public async Task<IActionResult> OrderByName(int id = 1)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All");
            }

            if (id <= 0)
            {
                return this.NotFound();
            }

            var orderedMovies = await this.moviesService.SortByNameAsync<AllMoviesViewModel>(id, ItemsPerPage);

            var orderedList = new ListAllMovies
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.moviesService.GetCount(),
                Movies = orderedMovies,
            };

            return this.View("OrderedMovies", orderedList);
        }

        public async Task<IActionResult> OrderByDate(int id = 1)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All");
            }

            if (id <= 0)
            {
                return this.NotFound();
            }

            var orderedMovies = await this.moviesService.SortByAddedAsync<AllMoviesViewModel>(id, ItemsPerPage);

            var orderedList = new ListAllMovies
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.moviesService.GetCount(),
                Movies = orderedMovies,
            };

            return this.View("OrderedMovies", orderedList);
        }

        public IActionResult OrderedMovies(ListAllMovies model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All");
            }

            var viewModel = model;
            return this.View(viewModel);
        }
    }
}
