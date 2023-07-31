namespace Adaptations.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Adaptations.Data.Models;
    using Adaptations.Services.Data;
    using Adaptations.Web.ViewModels.Books;
    using Adaptations.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : BaseController
    {
        private const int ItemsPerPage = 9;
        private readonly IBooksService booksService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment environment;

        public BooksController(
               IBooksService booksService,
               UserManager<ApplicationUser> userManager,
               IHostingEnvironment environment)
        {
            this.booksService = booksService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateBookInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.booksService.CreateAsync(model, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, $"Error: {ex.InnerException?.Message ?? ex.Message}");
                return this.View(model);
            }

            this.TempData["Message"] = "Book added successfully.";

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var books = await this.booksService.GetAllBooksAsync<AllBooksViewModel>(id, ItemsPerPage);


            var booksList = new ListAllBooks
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.booksService.GetCount(),
                Books = books,
            };
            return this.View(booksList);
        }

        public async Task<IActionResult> OrderByTitle(int id = 1)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All");
            }

            if (id <= 0)
            {
                return this.NotFound();
            }

            var orderedBooks = await this.booksService.SortByTitleAsync<AllBooksViewModel>(id, ItemsPerPage);

            var orderedList = new ListAllBooks
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.booksService.GetCount(),
                Books = orderedBooks,
            };

            return this.RedirectToAction("OrderedBooks", orderedList);
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

            var orderedBooks = await this.booksService.SortByAddedAsync<AllBooksViewModel>(id, ItemsPerPage);

            var orderedList = new ListAllBooks
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.booksService.GetCount(),
                Books = orderedBooks,
            };

            return this.RedirectToAction("OrderedBooks", orderedList);
        }

        public IActionResult OrderedBooks(ListAllBooks model)
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
