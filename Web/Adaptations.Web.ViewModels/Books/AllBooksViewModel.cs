namespace Adaptations.Web.ViewModels.Books
{
    using System.Linq;
    using Adaptations.Data.Common.Repositories;
    using Adaptations.Data.Models;
    using Adaptations.Data.Models.Enums;
    using Adaptations.Services.Mapping;
    using AutoMapper;

    public class AllBooksViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        //private readonly IDeletableEntityRepository<Book> booksRepository;

        //public AllBooksViewModel(IDeletableEntityRepository<Book> booksRepository)
        //{
        //    this.booksRepository = booksRepository;
        //}

        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public string Description { get; set; }

        public BookGenre Genre { get; set; }

        public string AuthorName { get; set; } /*=> this.booksRepository.All().Select(x => x.Author.Name).FirstOrDefault();*/

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, AllBooksViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/books/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))
                        .ForMember(x => x.AuthorName, opt =>
                            opt.MapFrom(x =>
                                x.Author.Name));
        }
    }
}
