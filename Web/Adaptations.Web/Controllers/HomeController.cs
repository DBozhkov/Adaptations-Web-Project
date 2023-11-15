namespace Adaptations.Web.Controllers
{
    using Adaptations.Services.Data;
    using Adaptations.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class HomeController : BaseController
    {
        private readonly IMoviesService movieService;

        public HomeController(IMoviesService moviesService)
        {
            this.movieService = moviesService;
        }

        public override async Task<IActionResult> Index()
        {
            this.ViewBag.RandomDictionary = await this.movieService.GetRandom();

            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
