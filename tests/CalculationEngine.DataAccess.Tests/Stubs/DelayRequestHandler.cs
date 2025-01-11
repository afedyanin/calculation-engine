using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculationEngine.DataAccess.Tests.Stubs;

internal class DelayRequestHandler : IRequestHandler<DelayRequest>
{
    private readonly ILogger<DelayRequestHandler> _logger;
    public DelayRequestHandler(ILogger<DelayRequestHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(DelayRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Starting delay for {request.CalculationUnitId}");
        await Task.Delay(request.Delay, cancellationToken);
        _logger.LogInformation($"Delay for {request.CalculationUnitId} completed.");
    }
}
