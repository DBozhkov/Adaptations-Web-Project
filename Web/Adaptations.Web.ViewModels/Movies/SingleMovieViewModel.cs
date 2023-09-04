namespace Adaptations.Web.ViewModels.Movies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Adaptations.Data.Models;
    using Adaptations.Data.Models.Enums;
    using Adaptations.Services.Mapping;
    using Adaptations.Web.ViewModels.Books;
    using AutoMapper;

    public class SingleMovieViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string MovieName { get; set; }

        public int ReleaseYear { get; set; }

        public string MoviePlot { get; set; }

        [Required]
        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }

        [Required]
        [Display(Name = "Runtime")]
        public int RunTime { get; set; }

        public double Rating { get; set; }

        public MovieGenre Genre { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<MovieActorViewModel> Actors { get; set; }

        public int BookId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Movie, SingleMovieViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/movies/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))
                .ForMember(x => x.Actors, opt =>
                       opt.MapFrom(x => x.ActorsMovies.Select(am => new MovieActorViewModel
                       {
                           Id = am.Actor.Id,
                           Name = am.Actor.Name,
                           Biography = am.Actor.Biography,
                       })))
                 .ForMember(x => x.BookId, opt =>
                        opt.MapFrom(x => x.Book.Id));
        }
    }
}
