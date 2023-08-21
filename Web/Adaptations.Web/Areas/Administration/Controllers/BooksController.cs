namespace Adaptations.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Adaptations.Services.Data;
    using Adaptations.Web.ViewModels.Books;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : AdministratorController
    {
        private readonly IBooksService booksService;

        public BooksController(
            IBooksService booksService)
        {
            this.booksService = booksService;
        }

        public IActionResult Edit(int id)
        {
            var inputModel = this.booksService.GetBookById<EditBookInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.booksService.EditAsync(id, model);
            return this.RedirectToAction("BookId", "Books", new { area = string.Empty, id });
        }

        public IActionResult Delete(int id)
        {
            var book = this.booksService.GetBookById<DeleteBookViewModel>(id);
            return this.View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteBookViewModel deleteBook)
        {
            var id = deleteBook.Id;
            await this.booksService.DeleteByIdAsync(id);
            return this.RedirectToAction("All", "Books", new { area = string.Empty });
        }
    }
}
