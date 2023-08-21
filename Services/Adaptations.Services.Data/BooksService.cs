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

            //if (inputBook.Characters == null)
            //{
            //    ingredient = new Ingredient { Name = inputIngredient.IngredientName };
            //}

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

            /// wwwroot / images / books / jhdsi - 343g3h453 -= g34g.jpg
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

        public async Task DeleteByIdAsync(int? id)
        {
            var book = this.booksRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            this.booksRepository.Delete(book);

            await this.booksRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int? id, EditBookInputModel model)
        {
            var book = this.booksRepository.All().FirstOrDefault(x => x.Id == id);
            book.Title = model.Title;
            book.ReleaseYear = model.ReleaseYear;
            book.Description = model.Description;
            book.Genre = model.Genre;
            book.BooksSold = model.BooksSold;
            book.Author.Name = model.AuthorName;
            book.Author.Biography = model.AuthorBiography;

            await this.booksRepository.SaveChangesAsync();
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
                 .Include(b => b.Characters)
                 .To<T>()
                 .ToListAsync();

            return books;
        }

        public T GetBookById<T>(int id)
        {
            var book = this.booksRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return book;
        }

        public async Task<IEnumerable<T>> GetBooksBySearchResult<T>(string searchResult, int page, int itemsPerPage = 9)
        {
            searchResult = searchResult ?? string.Empty;

            var models = await this.booksRepository.All()
                .Where(b => b.Title.ToLower().Contains(searchResult.ToLower()))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToArrayAsync();

            return models;
        }

        public bool IsSearchResultBook(string input)
        {
            return this.booksRepository.All().Any(x => x.Title.ToLower().Contains(input.ToLower()));
        }

        public int GetCount()
        {
            return this.booksRepository.All().Count();
        }

        public Task<IEnumerable<T>> GetRandom<T>(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> SortByTitleAsync<T>(int page, int itemsPerPage = 9)
        {
            var books = await this.booksRepository
                    .All()
                    .OrderBy(m => m.Title)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .To<T>()
                    .ToArrayAsync();

            return books;
        }

        public async Task<IEnumerable<T>> SortByAddedAsync<T>(int page, int itemsPerPage = 9)
        {
            var books = await this.booksRepository
                    .All()
                    .OrderBy(m => m.CreatedOn)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .To<T>()
                    .ToArrayAsync();

            return books;
        }
    }
}
