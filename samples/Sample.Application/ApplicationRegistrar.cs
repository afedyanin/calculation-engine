using Microsoft.Extensions.DependencyInjection;

namespace Sample.Application;

public static class ApplicationRegistrar
{
    public static IServiceCollection AddSampleApp(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistrar).Assembly));

        return services;
    }
}
