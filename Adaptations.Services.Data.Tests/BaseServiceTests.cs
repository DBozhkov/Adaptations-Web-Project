namespace Adaptations.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using Adaptations.Data;
    using Adaptations.Data.Common.Repositories;
    using Adaptations.Data.Models;
    using Adaptations.Data.Repositories;
    using Adaptations.Services.Mapping;
    using Adaptations.Services.Messaging;
    using Adaptations.Web.ViewModels.Account;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class BaseServiceTests : IDisposable
    {
        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        static BaseServiceTests()
        {
            InitializeAutoMapper();
        }

        protected BaseServiceTests()
        {
            var services = this.ConfigureServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }


        private ServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

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
                .AddRoles<ApplicationRole>()
                .AddDefaultTokenProviders();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISmsSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IMoviesService, MoviesService>();
            services.AddTransient<IBooksService, BooksService>();
            services.AddTransient<IActorsService, ActorsService>();

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor { HttpContext = context });

            return services;
        }

        private static void InitializeAutoMapper()
        {
            AutoMapperConfig.RegisterMappings(typeof(LoginViewModel).GetTypeInfo().Assembly);
        }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.ConfigureServices();
        }
    }
}
