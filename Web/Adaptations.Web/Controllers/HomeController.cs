namespace Adaptations.Web.Controllers
{
    using Adaptations.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountService countService;

        public HomeController(IGetCountService countService)
        {
            this.countService = countService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Movies()
        {
            return this.View();
        }

        public IActionResult Books()
        {
            var model = this.countService.GetAllCount();
            return this.View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
