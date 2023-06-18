namespace Adaptations.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Adaptations.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : BaseController
    {
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(MovieInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.Redirect("/");
        }
    }
}
