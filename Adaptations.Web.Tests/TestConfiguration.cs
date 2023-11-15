using Microsoft.Extensions.Configuration;

public class TestConfiguration
{
    public IConfiguration Configuration { get; }

    public TestConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Testing.json", optional: false, reloadOnChange: true);
        Configuration = builder.Build();
    }
}