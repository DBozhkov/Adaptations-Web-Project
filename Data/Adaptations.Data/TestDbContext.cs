using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Adaptations.Data.Common.Models;
using Adaptations.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class TestDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    private static readonly MethodInfo SetIsDeletedQueryFilterMethod = typeof(TestDbContext).GetMethod(
        nameof(SetIsDeletedQueryFilter),
        BindingFlags.NonPublic | BindingFlags.Static);

    public TestDbContext(DbContextOptions<TestDbContext> options, bool useInMemoryDatabase)
    : base(options)
    {
        UseInMemoryDatabase = useInMemoryDatabase;
    }

    public bool UseInMemoryDatabase { get; }

    public DbSet<Setting> Settings { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<ActorMovie> ActorsMovies { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Movie> Movies { get; set; }

    public override int SaveChanges() => this.SaveChanges(true);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.ApplyAuditInfoRules();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        this.SaveChangesAsync(true, cancellationToken);

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        this.ApplyAuditInfoRules();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Use in-memory database if configured to do so
            if (UseInMemoryDatabase)
            {
                optionsBuilder.UseInMemoryDatabase("InMemoryDatabaseName");
            }
            else
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Testing.json")
                    .Build();

                string connectionString = configuration.GetConnectionString("TestConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure your model relationships and entity mappings here

        // ... (Other configurations)

        // Apply the global query filter for not deleted entities
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(IDeletableEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, "IsDeleted");
                var condition = Expression.NotEqual(property, Expression.Constant(true));
                var lambda = Expression.Lambda(condition, parameter);

                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }

    private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
        where T : class, IDeletableEntity
    {
        builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }

    private void ApplyAuditInfoRules()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is IAuditInfo auditEntity)
            {
                var now = DateTime.UtcNow;

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntity.CreatedOn = now;
                        auditEntity.ModifiedOn = now;
                        break;

                    case EntityState.Modified:
                        entry.Property("CreatedOn").IsModified = false;
                        auditEntity.ModifiedOn = now;
                        break;
                }
            }
        }
    }
}