namespace Adaptations.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Adaptations.Services.Data;
    using Adaptations.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : AdministratorController
    {
        private readonly IMoviesService moviesService;

        public MoviesController(
            IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public IActionResult Edit(int id)
        {
            var inputModel = this.moviesService.GetMovieById<EditMovieInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditMovieInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.moviesService.EditAsync(id, model);
            return this.RedirectToAction("MovieId", "Movies", new { area = string.Empty, id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return this.View();
            //await this.moviesService.GetMovieById<DeleteMovieViewModel>(id);
            //return this.View(movieToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteMovieViewModel deleteMovie)
        {
            var id = deleteMovie.Id;
            await this.moviesService.DeleteByIdAsync(id);
            return this.RedirectToAction("Index", "Restaurants", new { area = string.Empty });
        }
    }
}
