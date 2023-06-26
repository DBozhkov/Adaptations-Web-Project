namespace Adaptations.Web.Controllers
{
    using Adaptations.Services.Data;
    using Adaptations.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountService countService;

        public HomeController(IGetCountService countService)
        {
            this.countService = countService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
