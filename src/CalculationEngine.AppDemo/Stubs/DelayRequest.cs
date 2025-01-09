using CalculationEngine.Core.Handlers;

namespace CalculationEngine.AppDemo.Stubs;

public class DelayRequest : ICalculationRequest
{
    public Guid CalculationUnitId { get; set; }

    public TimeSpan Delay { get; set; }
}
