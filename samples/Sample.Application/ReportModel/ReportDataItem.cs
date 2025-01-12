namespace Sample.Application.ReportModel;
public record class ReportDataItem
{
    public DateTime CreatedDate { get; set; }

    public required string Color { get; set; }

    public required string Name { get; set; }

    public required string JobId { get; set; }

    public int CalculationUnitIndex { get; set; }
}
