using CalculationEngine.Core.Handlers;

namespace CalculationEngine.Core.Tests.Stubs;

public class DelayRequest : ICalculationRequest
{
    public Guid CalculationUnitId { get; set; }

    public TimeSpan Delay { get; set; }
}
