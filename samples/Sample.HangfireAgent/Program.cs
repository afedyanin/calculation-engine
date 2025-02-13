
using CalculationEngine;
using Hangfire;
using Hangfire.PostgreSql;
using Sample.Application;

namespace Sample.HangfireAgent;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(
                c => c.UseNpgsqlConnection(builder.Configuration.GetConnectionString("HangfireConnection"))
                , new PostgreSqlStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromSeconds(5),
                    InvisibilityTimeout = TimeSpan.FromHours(24),
                    JobExpirationCheckInterval = TimeSpan.FromHours(24),
                }
        ));

        builder.Services.AddHangfireServer(options =>
        {
            options.StopTimeout = TimeSpan.FromSeconds(15);
            options.WorkerCount = 1;
            options.SchedulePollingInterval = TimeSpan.FromSeconds(5);
            options.HeartbeatInterval = TimeSpan.FromSeconds(10);
            options.ShutdownTimeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddCalculationEngine(
            builder.Configuration.GetConnectionString("CalculationEngineConnection"));

        builder.Services.AddSampleApp();

        var app = builder.Build();

        app.UseHangfireDashboard();

        app.Run();
    }
}
