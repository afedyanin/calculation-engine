using Microsoft.Extensions.DependencyInjection;

namespace CalculationEngine.AppDemo;

public static class ApplicationRegistrar
{
    public static IServiceCollection AddAppDemo(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistrar).Assembly));

        return services;
    }
}
