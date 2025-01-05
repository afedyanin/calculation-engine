using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalculationEngine.Core.Tests;
public abstract class HangfireClientTestBase
{
    private readonly IServiceCollection _services;
    private readonly ServiceProvider _serviceProvider;

    protected IConfiguration Configuration { get; }

    protected IBackgroundJobClient BackgroundJobClient { get; }

    protected HangfireClientTestBase()
    {
        Configuration = InitConfiguration();
        _services = new ServiceCollection();

        _services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(c =>
                c.UseNpgsqlConnection(Configuration.GetConnectionString("HangfireConnection")))
        );

        _serviceProvider = _services.BuildServiceProvider();

        BackgroundJobClient = _serviceProvider.GetRequiredService<IBackgroundJobClient>();
    }

    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
            .Build();

        return config;
    }
}
