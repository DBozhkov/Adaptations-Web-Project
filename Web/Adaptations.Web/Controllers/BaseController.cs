namespace Adaptations.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

    }
}
