namespace Adaptations.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Adaptations.Data;
    using Adaptations.Data.Common.Repositories;
    using Adaptations.Data.Models;
    using Adaptations.Data.Models.Enums;
    using Adaptations.Data.Repositories;
    using Adaptations.Web.ViewModels.Movies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Xunit;

    public class MovieServiceTests : BaseServiceTests
    {
        private IMoviesService MoviesServiceMock => this.ServiceProvider.GetRequiredService<IMoviesService>();

        [Fact]
        public async Task CreateMovieShouldActuallyCreateEntityIfInputIsValid()
        {
            var userId = "testUserId";
            var imagePath = "testImagePath";
            var testMovie = this.AddSingleMovie();

            await this.MoviesServiceMock.CreateAsync(testMovie, userId, imagePath);

            Assert.Equal(1, this.DbContext.Movies.Count());
        }

        [Fact]
        public async Task CreatedMovieNameShouldMatchTheInputMovieName()
        {
            var userId = "testUserId";
            var imagePath = "testImagePath";
            var testMovie = this.AddSingleMovie();

            await this.MoviesServiceMock.CreateAsync(testMovie, userId, imagePath);
            var expectedMovieName = this.DbContext.Movies.Select(x => x.MovieName).FirstOrDefault();

            Assert.Equal("Test Movie", expectedMovieName);
            Assert.Equal(1, this.DbContext.Movies.Count());
        }

        [Fact]
        public async Task CreateMovieShouldThrowExceptionIfNullValue()
        {
            var movieList = this.AddMultipleMovies();
            foreach (var movie in movieList)
            {
                var counter = 1;
                var userId = $"testUserId{counter}";
                var imagePath = $"testImagePath{counter}";

                await this.MoviesServiceMock.CreateAsync(movie, userId, imagePath);
                counter++;
            }

            Assert.Equal(10, this.DbContext.Movies.Count());
        }

        [Fact]
        public async Task AddingMultipleMoviesShouldWorkIfInputValuesAreCorrect()
        {
            var userId = "testUserId";
            var imagePath = "testImagePath";
            var testMovie = this.AddSingleMovie();

            testMovie.MovieName = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
             await this.MoviesServiceMock.CreateAsync(testMovie, userId, imagePath));
        }

        private CreateMovieInputModel AddSingleMovie()
        {
            var image1 = new FormFile(Stream.Null, 0, 0, "imageFile1", "image1.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var image2 = new FormFile(Stream.Null, 0, 0, "imageFile2", "image2.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var movie = new CreateMovieInputModel
            {
                MovieName = "Test Movie",
                DirectorName = "Test Director Name",
                ReleaseYear = 2000,
                MoviePlot = "Test Plot",
                Genre = MovieGenre.Comedy,
                Rating = 7,
                RunTime = 111,
                Actors = new List<ActorInputModel>
                {
                    new ActorInputModel
                    {
                    Name = "Test Actor Name",
                    Biography = "Test Actor Bio",
                    },
                    new ActorInputModel
                    {
                        Name = "Second Test Name",
                        Biography = "Second Test Bio",
                    },
                },
                BookTitle = "Test BookTitle",
                Images = new List<IFormFile> { image1, image2 },
            };

            return movie;
        }

        private List<CreateMovieInputModel> AddMultipleMovies()
        {
            var list = new List<CreateMovieInputModel>();
            for (int i = 1; i <= 10; i++)
            {
                var image1 = new FormFile(Stream.Null, 0, 0, $"imageFile{i}", $"image{i}.jpg")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg",
                };
                var image2 = new FormFile(Stream.Null, 0, 0, $"secondImageFile{i}", $"secondImage{i}.jpg")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg",
                };
                var rand = new Random();

                var movie = new CreateMovieInputModel
                {
                    MovieName = $"Test Movie{i}",
                    DirectorName = $"Test Director Name{i}",
                    ReleaseYear = 2000 + i,
                    MoviePlot = $"Test Plot{i}",
                    Genre = MovieGenre.Comedy,
                    Rating = rand.Next(1, 10),
                    RunTime = 111 + i,
                    Actors = new List<ActorInputModel>
                {
                    new ActorInputModel
                    {
                    Name = $"Test Actor Name{i}",
                    Biography = $"Test Actor Bio{i}",
                    },
                    new ActorInputModel
                    {
                        Name = $"Second Test Name{i}",
                        Biography = $"Second Test Bio{i}",
                    },
                },
                    BookTitle = $"Test BookTitle{i}",
                    Images = new List<IFormFile> { image1, image2 },
                };

                list.Add(movie);
            }

            return list;
        }
    }
}
