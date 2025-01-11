using CalculationEngine.Core.Handlers;

namespace CalculationEngine.DataAccess.Tests.Stubs;

public class DelayRequest : ICalculationRequest
{
    public Guid CalculationUnitId { get; set; }

    public TimeSpan Delay { get; set; }
}
