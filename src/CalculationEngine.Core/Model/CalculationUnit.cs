using CalculationEngine.Core.Handlers;

namespace CalculationEngine.Core.Model;

public class CalculationUnit
{
    public Guid Id { get; set; }

    public Guid GraphId { get; set; }

    public required ICalculationRequest Request { get; set; }

    public string? JobId { get; set; }

    public IEnumerable<CalculationResultItem> Results { get; set; } = [];
}
