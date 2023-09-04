namespace Adaptations.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IActorsService
    {
        T GetActorById<T>(int id);

        int GetMoviesCount(int id);

        Task<IEnumerable<T>> GetAllMoviesByActorId<T>(int id);

        string BioSummary(int id);
    }
}
