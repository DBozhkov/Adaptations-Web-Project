namespace Adaptations.Web.ViewModels.Actors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Adaptations.Data.Models;
    using Adaptations.Services.Mapping;
    using AutoMapper;

    public class SingleActorViewModel : IMapFrom<Actor>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(10, 500)]
        public string Biography { get; set; }

        public string ShortBio { get; set; }

        public string Country { get; set; }

        public DateTime BornOn { get; set; }

        public DateTime DiedOn { get; set; }

        public string ImageUrl { get; set; }

        //public string MovieImageUrl { get; set; }

        public IEnumerable<ActorMovieViewModel> Movies { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Actor, SingleActorViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.FirstOrDefault().RemoteImageUrl :
                        "/images/actors/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))
                //.ForMember(x => x.MovieImageUrl, opt =>
                //        opt.MapFrom(x =>
                //                x.ActorsMovies
                //                .Select(am => am.Movie.Images.FirstOrDefault().RemoteImageUrl != null ?
                //        x.Images.FirstOrDefault().RemoteImageUrl :
                //        "/images/movies/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension)))
                .ForMember(x => x.Movies, opt =>
                       opt.MapFrom(src => src.ActorsMovies.Select(am => new ActorMovieViewModel
                       {
                           Id = am.Movie.Id,
                           Name = am.Movie.MovieName,
                           ImageUrl = am.Movie.Images.FirstOrDefault().RemoteImageUrl != null ?
                        am.Movie.Images.FirstOrDefault().RemoteImageUrl :
                   "/images/movies/" + am.Movie.Images.FirstOrDefault().Id + "." + am.Movie.Images
                   .FirstOrDefault().Extension,
                           ReleasedOn = am.Movie.ReleaseYear,
                       })));
        }
    }
}
