namespace CalculationEngine.Core.Model;

public class CalculationResultItem
{
    public Guid Id { get; set; }

    public Guid CalculationUnitId { get; set; }

    public string Name { get; set; }

    public string Metadata { get; set; }

    public string PayloadJson { get; set; }
}
