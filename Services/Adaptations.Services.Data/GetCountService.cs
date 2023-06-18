using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adaptations.Data.Common.Repositories;
using Adaptations.Data.Models;
using Adaptations.Web.ViewModels;

namespace Adaptations.Services.Data
{
    public class GetCountService : IGetCountService
    {
        private readonly IDeletableEntityRepository<Book> bookRepository;

        public GetCountService(IDeletableEntityRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IndexViewModel GetAllCount()
        {
            var model = new IndexViewModel
            {
                BooksCount = this.bookRepository.All().Count(),
            };

            return model;
        }
    }
}
