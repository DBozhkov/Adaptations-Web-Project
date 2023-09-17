namespace Adaptations.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class BaseController : Controller
    {
        public virtual async Task<IActionResult> Index()
        {
            return this.View();
        }

    }
}
