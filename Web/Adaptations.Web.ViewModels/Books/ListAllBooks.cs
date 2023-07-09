namespace Adaptations.Web.ViewModels.Books
{
    using System.Collections.Generic;

    public class ListAllBooks : PageViewModel
    {
        public IEnumerable<AllBooksViewModel> Books { get; set; }
    }
}
