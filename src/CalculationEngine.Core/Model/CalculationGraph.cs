using CalculationEngine.Graphlib;
namespace CalculationEngine.Core.Model;

public class CalculationGraph : Graph<CalculationUnit>
{
    public Guid Id { get; set; }
}

