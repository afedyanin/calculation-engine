using CalculationEngine.Core.Handlers;
using CalculationEngine.Core.HangfireExtensions;
using CalculationEngine.Core.Repositories;
using CalculationEngine.DataAccess;
using CalculationEngine.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CalculationEngine;

public static class CalculationEngineRegistrar
{
    public static IServiceCollection AddCalculationEngine(
        this IServiceCollection services, string? connectionString)
    {
        // Mediatr stuff
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EnqueueGraphRequest).Assembly));

        // Hangfire stuff
        services.AddScoped<IJobScheduler, HangfireJobScheduler>();

        // DAL stuff
        services.AddTransient<ICalculationGraphRepository, CalculationGraphRepository>();
        services.AddTransient<ICalculationUnitRepository, CalculationUnitRepository>();
        services.AddTransient<ICalculationResultRepository, CalculationResultRepository>();

        services.AddDbContextFactory<CalculationEngineDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}
