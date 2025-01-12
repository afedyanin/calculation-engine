using CalculationEngine.Core.Handlers;

namespace Sample.Application.CalculationUnits;
public class ColoredCalcRequest : ICalculationRequest
{
    public Guid CalculationUnitId { get; set; }

    public required string Color { get; set; }

    public TimeSpan Delay { get; set; }

}
