namespace Adaptations.Web.ViewModels.Books
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Adaptations.Data.Models;
    using Adaptations.Data.Models.Enums;
    using Adaptations.Services.Mapping;
    using AutoMapper;

    public class SingleBookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(1900, 2023)]
        public int ReleaseYear { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public BookGenre Genre { get; set; }

        public int BooksSold { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<CharacterViewModel> Characters { get; set; }

        public int MovieId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, SingleBookViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/books/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))
                .ForMember(x => x.AuthorName, opt =>
                            opt.MapFrom(x =>
                                x.Author.Name))
                 .ForMember(x => x.MovieId, opt =>
                            opt.MapFrom(x =>
                                x.Movie.Id));
                 //.ForMember(x => x.Characters, opt =>
                 //           opt.MapFrom(x =>
                 //               x.Characters.Select(c => new CharacterViewModel
                 //               {
                 //                   CharacterName = c.CharacterName,
                 //                   CharacterDescription = c.CharacterDescription,
                 //               }).ToList()));
        }
    }
}
