using Adaptations.Data.Common.Models;
using Adaptations.Data.Common.Repositories;
using Adaptations.Data.Models;
using Adaptations.Services.Mapping;
using Adaptations.Web.ViewModels.Movies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Adaptations.Services.Data
{
    public class MoviesService : IMoviesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Movie> movieRepository;

        public MoviesService(IDeletableEntityRepository<Movie> movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task CreateAsync(CreateMovieInputModel inputMovie, string userId, string imagePath)
        {
            var movie = new Movie
            {
                MovieName = inputMovie.MovieName,
                ReleaseYear = inputMovie.ReleaseYear,
                MoviePlot = inputMovie.MoviePlot,
                Genre = inputMovie.Genre,
                Rating = inputMovie.Rating,
                AddedByUserId = userId,
            };

            /// wwwroot / images / recipes / jhdsi - 343g3h453 -= g34g.jpg
            Directory.CreateDirectory($"{imagePath}/movies/");
            foreach (var image in inputMovie.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var formImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                movie.Images.Add(formImage);

                var physicalPath = $"{imagePath}/movies/{formImage.Id}.{extension}";
                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                };
            }

            await this.movieRepository.AddAsync(movie);
            await this.movieRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.movieRepository.All().Count();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task EditAsync(MovieViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllMoviesAsync<T>(int page, int itemsPerPage = 9)
        {
            var movies = await this.movieRepository
                 .All()
                 .Skip((page - 1) * itemsPerPage)
                 .Take(itemsPerPage)
                 .To<T>()
                 .ToArrayAsync();
            return movies;
        }

        public Task<IEnumerable<MovieViewModel>> GetAllMoviesByActor()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MovieViewModel>> GetAllMoviesByBook()
        {
            throw new System.NotImplementedException();
        }

        public Task<MovieViewModel> GetMovieByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MovieViewModel>> GetMoviesBySearchResult(string searchResult, int? movieId)
        {
            throw new System.NotImplementedException();
        }


        Task<IEnumerable<T>> IMoviesService.GetRandom<T>(int count)
        {
            throw new NotImplementedException();
        }
    }
}
