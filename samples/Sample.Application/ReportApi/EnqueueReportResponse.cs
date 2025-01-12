namespace Sample.Application.ReportApi;
public class EnqueueReportResponse
{
    public Guid? GraphId { get; set; }

    public string? JobId { get; set; }

    public Guid? ReportResultCalculationUnitId { get; set; }

    public bool Success => GraphId != null;

    public string? Message { get; set; }

    public string? GraphVerticesTraversal { get; set; }
}
