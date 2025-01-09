using MediatR;

namespace CalculationEngine.Core.Model;

public class CalculationUnit
{
    public Guid Id { get; set; }

    public Guid GraphId { get; set; }

    public required IRequest Request { get; set; }

    public string? JobId { get; set; }

    public IEnumerable<CalculationResultItem> CalculationResults { get; set; } = [];
}
