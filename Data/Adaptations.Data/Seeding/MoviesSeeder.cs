namespace Adaptations.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class MoviesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Movies.Any())
            {
                return;
            }

            //await dbContext.Movies.AddAsync(new Movie { Name = "Тарт" });
            //await dbContext.Movies.AddAsync(new Movie { Name = "Кекс" });
            //await dbContext.Movies.AddAsync(new Movie { Name = "Печено Свинско" });

            await dbContext.SaveChangesAsync();
        }
    }
}