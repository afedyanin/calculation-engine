using Microsoft.Extensions.DependencyInjection;

namespace CalculationEngine.Core;

public static class CalculationEngineRegistrar
{
    public static IServiceCollection AddCalculationEngine(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CalculationEngineRegistrar).Assembly));

        return services;
    }
}
