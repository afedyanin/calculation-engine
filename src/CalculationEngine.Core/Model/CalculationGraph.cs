using System.Text;
using CalculationEngine.Graphlib;
using CalculationEngine.Graphlib.Algos;
namespace CalculationEngine.Core.Model;

public class CalculationGraph : Graph<CalculationUnit>
{
    public Guid Id { get; set; }
}

