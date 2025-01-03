using CalcEngine.Application.Handlers.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CalcEngine.Application;

public static class ApplicationRegistrar
{
    public static IServiceCollection AddCalcEngineApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GenerateReportRequest).Assembly));

        return services;
    }
}
