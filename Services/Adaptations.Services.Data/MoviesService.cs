namespace Adaptations.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Adaptations.Data.Common.Repositories;
    using Adaptations.Data.Models;
    using Adaptations.Services.Mapping;
    using Adaptations.Web.ViewModels.Movies;
    using Microsoft.EntityFrameworkCore;

    public class MoviesService : IMoviesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Movie> movieRepository;
        private readonly IDeletableEntityRepository<Book> bookRepository;
        private Dictionary<string, string> getBookTitles;

        public MoviesService(
            IDeletableEntityRepository<Movie> movieRepository,
            IDeletableEntityRepository<Book> bookRepository)
        {
            this.movieRepository = movieRepository;
            this.bookRepository = bookRepository;
            this.getBookTitles = new Dictionary<string, string>();
        }

        public async Task CreateAsync(CreateMovieInputModel inputMovie, string userId, string imagePath)
        {
            var movie = new Movie
            {
                MovieName = inputMovie.MovieName,
                DirectorName = inputMovie.DirectorName,
                ReleaseYear = inputMovie.ReleaseYear,
                MoviePlot = inputMovie.MoviePlot,
                Genre = inputMovie.Genre,
                Rating = inputMovie.Rating,
                RunTime = inputMovie.RunTime,
                AddedByUserId = userId,
            };

            if (inputMovie.Actors != null && inputMovie.Actors.Any())
            {
                foreach (var actorInput in inputMovie.Actors)
                {
                    var actor = new Actor
                    {
                        Name = actorInput.Name,
                        Biography = actorInput.Biography,
                    };

                    movie.ActorsMovies.Add(new ActorMovie
                    {
                        Actor = actor,
                    });
                }
            }

            if (inputMovie.BookTitle.Length > 0 && inputMovie.BookTitle != null)
            {
                var book = this.bookRepository
                        .All()
                        .Where(x => x.Title == inputMovie.BookTitle)
                        .FirstOrDefault();

                if (book != null)
                {
                    movie.Book = book;
                }
                else
                {
                    this.getBookTitles[movie.MovieName] = inputMovie.BookTitle;
                }
            }

            /// wwwroot / images / movies / jhdsi - 343g3h453 -= g34g.jpg
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

        public async Task DeleteByIdAsync(int? id)
        {
            var movie = this.movieRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            this.movieRepository.Delete(movie);

            await this.movieRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int? id, EditMovieInputModel model)
        {
            var movie = this.movieRepository.All().FirstOrDefault(x => x.Id == id);
            movie.MovieName = model.MovieName;
            movie.MoviePlot = model.MoviePlot;
            movie.DirectorName = model.DirectorName;
            movie.RunTime = model.RunTime;
            movie.ReleaseYear = model.ReleaseYear;
            movie.Rating = model.Rating;
            movie.Genre = model.Genre;

            await this.movieRepository.SaveChangesAsync();
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

        public async Task<IEnumerable<T>> GetAllActorsByMovieId<T>(int id)
        {
            var actors = await this.movieRepository
                 .All()
                 .Where(x => x.ActorsMovies.Any(am => am.ActorId == id))
                 .To<T>()
                 .ToListAsync();

            return actors;
        }

        public async Task<IEnumerable<T>> GetAllBooksByMovieId<T>(int id)
        {
            var movies = await this.movieRepository
                 .All()
                 .Where(x => x.Book.Id == id)
                 .To<T>()
                 .ToListAsync();

            return movies;
        }

        public T GetMovieById<T>(int id)
        {
            var movie = this.movieRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return movie;
        }

        public async Task<IEnumerable<T>> GetMoviesBySearchResultAsync<T>(string searchResult, int page, int itemsPerPage = 9)
        {
            searchResult = searchResult ?? string.Empty;

            var models = await this.movieRepository.All()
                .Where(m => m.MovieName.ToLower().Contains(searchResult.ToLower()))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToArrayAsync();

            return models;
        }

        public bool IsSearchResultMovie(string input)
        {
            return this.movieRepository.All().Any(x => x.MovieName.ToLower().Contains(input.ToLower()));
        }


        public async Task<Dictionary<int, string>> GetRandom()
        {
            var movieIds = await this.movieRepository
                .All()
                .Select(x => new
                {
                    x.Id,
                    ImageUrl = x.Images.FirstOrDefault().RemoteImageUrl != null ?
                         x.Images.FirstOrDefault().RemoteImageUrl :
                         "/images/movies/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension,
                })
                .ToDictionaryAsync(x => x.Id, x => x.ImageUrl);

            var orderedIds = movieIds.OrderBy(x => Guid.NewGuid()).ToDictionary(x => x.Key, x => x.Value);

            return orderedIds;
        }

        public async Task<IEnumerable<T>> SortByNameAsync<T>(int page, int itemsPerPage = 9)
        {
            var movies = await this.movieRepository
                         .All()
                         .OrderBy(m => m.MovieName)
                         .Skip((page - 1) * itemsPerPage)
                         .Take(itemsPerPage)
                         .To<T>()
                         .ToArrayAsync();

            return movies;
        }

        public async Task<IEnumerable<T>> SortByAddedAsync<T>(int page, int itemsPerPage = 9)
        {
            var movies = await this.movieRepository
             .All()
             .OrderBy(m => m.CreatedOn)
             .Skip((page - 1) * itemsPerPage)
             .Take(itemsPerPage)
             .To<T>()
             .ToArrayAsync();

            return movies;
        }

        public int GetBookId(int id)
        {
            var movie = this.movieRepository
                         .All()
                         .Where(x => x.Id == id)
                         .FirstOrDefault();

            var bookTitle = this.getBookTitles[movie.MovieName];

            var book = this.bookRepository
                        .All()
                        .Where(x => x.Title == bookTitle)
                        .FirstOrDefault();

            var bookId = book.Id;

            if (movie.Book.Id != bookId)
            {
                movie.Book = book;
            }

            return bookId;
        }
    }
}
