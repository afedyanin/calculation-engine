using MediatR;

namespace CalculationEngine.Core.Handlers;

public class JobAwaitingRequest : IRequest
{
    public Guid CorrelationId { get; set; }

    public string[] JobIds { get; set; } = [];

    public TimeSpan PoolingInterval { get; set; }
}
