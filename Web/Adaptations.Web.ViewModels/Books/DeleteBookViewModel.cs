using System.Collections.Generic;
using System.Linq;
using Adaptations.Data.Models;
using Adaptations.Data.Models.Enums;
using Adaptations.Services.Mapping;
using AutoMapper;

namespace Adaptations.Web.ViewModels.Books
{
    public class DeleteBookViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public string Description { get; set; }

        public BookGenre Genre { get; set; }

        public string AuthorName { get; set; }

        public string AuthorBiography { get; set; }

        public int BooksSold { get; set; }

        public IEnumerable<CharacterInputModel> Characters { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, EditBookInputModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/books/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
