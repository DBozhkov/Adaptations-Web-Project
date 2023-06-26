namespace Adaptations.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Adaptations.Web.ViewModels;
    using Adaptations.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : BaseController
    {
        public IActionResult Index(IndexViewModel model)
        {
            return this.View(model);
        }
    }
}
