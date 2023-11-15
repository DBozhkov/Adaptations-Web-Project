using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
        RootUri = host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.LastOrDefault();

        return new TestServer(builder);
    }

    protected IWebHostBuilder CreateWebHostBuilder()
    {
        // Use the Startup class from your application
        return new WebHostBuilder()
            .UseStartup<TStartup>()
            .UseEnvironment("Development") // Set the desired environment
            .UseConfiguration(new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build());
    }

    public void Dispose()
    {
        host.Dispose();
        process.CloseMainWindow();
    }
}