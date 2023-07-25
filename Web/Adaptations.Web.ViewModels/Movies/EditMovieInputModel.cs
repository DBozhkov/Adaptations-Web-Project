namespace Adaptations.Web.ViewModels.Movies
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Adaptations.Data.Models;
    using Adaptations.Data.Models.Enums;
    using Adaptations.Services.Mapping;
    using AutoMapper;

    public class EditMovieInputModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Movie Name")]
        public string MovieName { get; set; }

        [Required]
        [Display(Name = "Plot")]
        public string MoviePlot { get; set; }

        [Required]
        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }

        [Required]
        [Display(Name = "Runtime")]
        public int RunTime { get; set; }

        [Required]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [Required]
        public double Rating { get; set; }

        public MovieGenre Genre { get; set; }

        public string ImageUrl { get; set; }

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
