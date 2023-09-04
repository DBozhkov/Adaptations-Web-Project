namespace Adaptations.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Adaptations.Data.Common.Repositories;
    using Adaptations.Data.Models;
    using Adaptations.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ActorsService : IActorsService
    {
        private readonly IRepository<Actor> actorsRepository;

        public ActorsService(IRepository<Actor> actorsRepository)
        {
            this.actorsRepository = actorsRepository;
        }

        public string BioSummary(int id)
        {
            var actor = this.actorsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            var shortBio = string.Empty;

            if (actor != null && !string.IsNullOrEmpty(actor.Biography))
            {
                if (actor.Biography.Length <= 50)
                {
                    shortBio = actor.Biography.Substring(0);
                }
                else
                {
                    shortBio = actor.Biography.Substring(0, 50);
                }
            }

            return shortBio;
        }

        public T GetActorById<T>(int id)
        {
            var actor = this.actorsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return actor;
        }

        public async Task<IEnumerable<T>> GetAllMoviesByActorId<T>(int id)
        {
            var movies = await this.actorsRepository
                 .All()
                 .Where(x => x.ActorsMovies.Any(am => am.ActorId == id))
                 .To<T>()
                 .ToListAsync();

            return movies;
        }

        public int GetMoviesCount(int id)
        {
            var count = this.actorsRepository
                    .All()
                    .Where(x => x.Id == id)
                    .SelectMany(x => x.ActorsMovies
                    .Select(am => am.Movie.Id))
                    .Distinct()
                    .Count();

            return count;
        }
    }
}
