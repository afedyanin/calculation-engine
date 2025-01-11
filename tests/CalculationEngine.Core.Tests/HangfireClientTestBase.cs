using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using CalculationEngine.Core.HangfireExtensions;

namespace CalculationEngine.Core.Tests;
public abstract class HangfireClientTestBase
{
    private readonly IServiceCollection _services;

    private readonly ServiceProvider _serviceProvider;

    protected IConfiguration Configuration { get; }

    protected IJobScheduler JobScheduler { get; }

    protected IRecurringJobManager RecurringJobClient { get; }

    protected IBackgroundJobClient BackgroundJobClient { get; }

    protected IMonitoringApi MonitoringApi { get; }

    protected IStorageConnection JobStorageConnection { get; }

    protected IMediator Mediator { get; }

    protected ILogger<T> GetLogger<T>() => _serviceProvider.GetRequiredService<ILogger<T>>();

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

        _services.AddScoped<IJobScheduler, HangfireJobScheduler>();

        _services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(HangfireClientTestBase).Assembly));

        _services.AddLogging(builder => builder.AddConsole());

        //_services.AddAppDemo();

        _serviceProvider = _services.BuildServiceProvider();
        BackgroundJobClient = _serviceProvider.GetRequiredService<IBackgroundJobClient>();
        JobScheduler = _serviceProvider.GetRequiredService<IJobScheduler>();
        RecurringJobClient = _serviceProvider.GetRequiredService<IRecurringJobManager>();
        MonitoringApi = JobStorage.Current.GetMonitoringApi();
        JobStorageConnection = JobStorage.Current.GetConnection();
        Mediator = _serviceProvider.GetRequiredService<IMediator>();
    }

    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
            .Build();

        return config;
    }
}
