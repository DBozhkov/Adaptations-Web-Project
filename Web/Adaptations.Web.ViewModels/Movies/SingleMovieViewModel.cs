using Adaptations.Data.Models;
using Adaptations.Data.Models.Enums;
using Adaptations.Services.Mapping;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Adaptations.Web.ViewModels.Movies
{
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

        public IEnumerable<ActorMovieViewModel> Actors { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Movie, SingleMovieViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/movies/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
