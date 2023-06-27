using Adaptations.Data.Models;
using Adaptations.Data.Models.Enums;
using Adaptations.Services.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adaptations.Web.ViewModels.Movies
{
    public class AllMoviesViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int MovieId { get; set; }

        public string ImageUrl { get; set; }

        public string MovieName { get; set; }

        public int ReleaseYear { get; set; }

        public string MoviePlot { get; set; }

        public double Rating { get; set; }

        public MovieGenre Genre { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Movie, AllMoviesViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/movies/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
