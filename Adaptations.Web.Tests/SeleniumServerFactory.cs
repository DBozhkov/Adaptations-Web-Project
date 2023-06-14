namespace Adaptations.Web.Tests
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server.Features;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;

    namespace Adaptations.Web.Tests
    {
        public sealed class SeleniumServerFactory<TStartup>
            where TStartup : class
        {
            private readonly Process process;
            private IWebHost host;

            public SeleniumServerFactory()
            {
                CreateServer(CreateWebHostBuilder());

                process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "selenium-standalone",
                        Arguments = "start",
                        UseShellExecute = true
                    }
                };
                process.Start();
            }

            public string RootUri { get; set; }

            public TestServer CreateServer(IWebHostBuilder builder)
            {
                host = builder.Build();
                host.Start();
                RootUri = host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.LastOrDefault(); // Last is https://localhost:5001!

                // Return the test server
                return new TestServer(new WebHostBuilder()
                    .ConfigureServices(services =>
                    {
                        services.AddSingleton<TStartup>(provider => host.Services.GetRequiredService<TStartup>());
                    })
                    .Configure(app =>
                    {
                        app.UseMvc();
                    }));
            }

            protected IWebHostBuilder CreateWebHostBuilder()
            {
                return new WebHostBuilder()
                    .UseStartup<FakeStartup>();
            }

            public void Dispose()
            {
                host.Dispose();
                process.CloseMainWindow(); // Be sure to stop Selenium Standalone
            }

            public class FakeStartup
            {
                public void ConfigureServices(IServiceCollection services)
                {
                }

                public void Configure(IApplicationBuilder app, IHostingEnvironment env)
                {
                    app.UseMvc();
                }
            }
        }
    }
}
