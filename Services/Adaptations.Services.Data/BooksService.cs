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
    using Adaptations.Web.ViewModels.Books;
    using Adaptations.Web.ViewModels.Movies;
    using Microsoft.EntityFrameworkCore;

    public class BooksService : IBooksService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Book> booksRepository;

        public BooksService(
                            IDeletableEntityRepository<Book> booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        public async Task CreateAsync(CreateBookInputModel inputBook, string userId, string imagePath)
        {
            var book = new Book
            {
                Title = inputBook.Title,
                ReleaseYear = inputBook.ReleaseYear,
                Description = inputBook.Description,
                Author = new Author
                {
                    Name = inputBook.AuthorName,
                    Biography = inputBook.AuthorBiography,
                },
                Genre = inputBook.Genre,
                BooksSold = inputBook.BooksSold,
                AddedByUserId = userId,
            };

            if (inputBook.Characters != null && inputBook.Characters.Any())
            {
                foreach (var characterInput in inputBook.Characters)
                {
                    var character = new Character
                    {
                        CharacterName = characterInput.Name,
                        CharacterDescription = characterInput.Description,
                    };

                    book.Characters.Add(character);
                }
            }

            /// wwwroot / images / movies / jhdsi - 343g3h453 -= g34g.jpg
            Directory.CreateDirectory($"{imagePath}/books/");
            foreach (var image in inputBook.Images)
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
                book.Images.Add(formImage);

                var physicalPath = $"{imagePath}/books/{formImage.Id}.{extension}";
                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                };
            }

            await this.booksRepository.AddAsync(book);
            await this.booksRepository.SaveChangesAsync();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(int? id, EditMovieInputModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllBooksAsync<T>(int page, int itemsPerPage = 9)
        {
            var books = await this.booksRepository
                     .All()
                     .OrderBy(x => x.Title)
                     .Skip((page - 1) * itemsPerPage)
                     .Take(itemsPerPage)
                     .To<T>()
                     .ToArrayAsync();

            return books;
        }

        public async Task<IEnumerable<T>> GetAllMoviesByBookId<T>(int id)
        {
            var books = await this.booksRepository
                 .All()
                 .Where(x => x.Movie.Id == id)
                 .To<T>()
                 .ToListAsync();

            return books;
        }

        public T GetBookAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieViewModel>> GetBooksBySearchResult(string searchResult, int? movieId)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return this.booksRepository.All().Count();
        }

        public Task<IEnumerable<T>> GetRandom<T>(int count)
        {
            throw new NotImplementedException();
        }
    }
}
