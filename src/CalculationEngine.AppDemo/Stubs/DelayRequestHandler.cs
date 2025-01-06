using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculationEngine.AppDemo.Stubs;

internal class DelayRequestHandler : IRequestHandler<DelayRequest>
{
    private readonly ILogger<DelayRequestHandler> _logger;
    public DelayRequestHandler(ILogger<DelayRequestHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(DelayRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Starting delay for {request.CorrelationId}");
        await Task.Delay(request.Delay, cancellationToken);
        _logger.LogInformation($"Delay for {request.CorrelationId} completed.");
    }
}
