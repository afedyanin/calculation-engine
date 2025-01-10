using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CalculationEngine.DataAccess.Tests;

public class DbTestBase
{
    private readonly IServiceCollection _services;
    private readonly ServiceProvider _serviceProvider;

    protected IConfiguration Configuration { get; }

    protected IDbContextFactory<CalculationEngineDbContext> ContextFactory { get; }

    public DbTestBase()
    {
        Configuration = InitConfiguration();
        _services = new ServiceCollection();

        _services.AddLogging(builder => builder.AddConsole());

        _services.AddDbContextFactory<CalculationEngineDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("CalculationEngineConnection")));

        _serviceProvider = _services.BuildServiceProvider();

        ContextFactory = _serviceProvider.GetRequiredService<IDbContextFactory<CalculationEngineDbContext>>();
    }

    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
            .Build();

        return config;
    }
}
