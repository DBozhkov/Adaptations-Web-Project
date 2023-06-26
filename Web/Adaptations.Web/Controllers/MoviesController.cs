namespace Adaptations.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Adaptations.Data.Models;
    using Adaptations.Services.Data;
    using Adaptations.Web.ViewModels.Movies;
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

        public async Task<IActionResult> All(ListAllMovies allMovies)
        {
            var movies = await this.moviesService.GetAllMoviesAsync();

            var movielist = new ListAllMovies
            {
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
                await this.moviesService.CreateAsync(model, user.Id /*$"{this.environment.WebRootPath}/images"*/);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            this.TempData["Message"] = "Recipe added successfully.";

            return this.RedirectToAction("All");
        }
    }
}
