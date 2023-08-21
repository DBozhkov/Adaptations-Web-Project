namespace Sandbox
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Adaptations.Data;
    using Adaptations.Data.Common;
    using Adaptations.Data.Common.Repositories;
    using Adaptations.Data.Models;
    using Adaptations.Data.Repositories;
    using Adaptations.Data.Seeding;
    using Adaptations.Services.Data;
    using Adaptations.Services.Messaging;

    using CommandLine;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);


            // Seed data on application startup
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;

                return Parser.Default.ParseArguments<SandboxOptions>(args).MapResult(
                    (SandboxOptions opts) => SandboxCode(opts, serviceProvider),
                    _ => 255);
            }
        }

        private static int SandboxCode(SandboxOptions options, IServiceProvider serviceProvider)
        {
            var sw = Stopwatch.StartNew();
            var settingsService = serviceProvider.GetService<ISettingsService>();
            Console.WriteLine($"Count of settings: {settingsService.GetCount()}");

            var dbContext = serviceProvider.GetService<ApplicationDbContext>();

            // Query the database for character data
            var books = dbContext.Books.Include(b => b.Characters).ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
                var bookType = book.GetType();
                var properties = bookType.GetProperties();
                foreach (var property in properties)
                {
                    //if (property.Name == "Characters")
                    //{
                    //    var characters = property.GetValue(book);
                    //    if (characters != null)
                    //    {
                    //        Type listType = property.PropertyType.GetGenericArguments()[0];
                    //        var countProperty = listType.GetProperty("Count");
                    //        object countValue = countProperty.GetValue(characters);
                    //        int count = (int)countValue;
                    //        Console.WriteLine($"Count of Characters: {count}");
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine($"Characters is null.");
                    //    }
                    //}
                    Console.WriteLine($"{property.Name}");
                }
                Console.WriteLine($"{book.Characters.Count()}");
                foreach (var chr in book.Characters)
                {
                    var charName = chr.CharacterName;
                    var charDescr = chr.CharacterDescription;
                    Console.WriteLine(charName + " " + charDescr);
                }
            }
            Console.WriteLine(sw.Elapsed);
            return 0;
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .UseLoggerFactory(new LoggerFactory()));

            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISmsSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IMoviesService, MoviesService>();
            services.AddTransient<IBooksService, BooksService>();
        }
    }
}
