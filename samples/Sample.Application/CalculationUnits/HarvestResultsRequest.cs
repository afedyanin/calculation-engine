using CalculationEngine.Core.Handlers;

namespace Sample.Application.CalculationUnits;
public class HarvestResultsRequest : ICalculationRequest
{
    public Guid CalculationUnitId { get; set; }
}
