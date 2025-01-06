using MediatR;

namespace CalculationEngine.AppDemo.Stubs;

public class DelayRequest : IRequest
{
    public Guid CorrelationId { get; set; }
    public TimeSpan Delay { get; set; }
}
