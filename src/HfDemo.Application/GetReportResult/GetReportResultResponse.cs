namespace HfDemo.Application.GetReportResult;

public class GetReportResultResponse
{
    public static readonly GetReportResultResponse EmptyResult = new()
    {
        ReportId = Guid.Empty,
        ReportResult = string.Empty,
    };

    public Guid ReportId { get; set; }

    public string ReportResult { get; set; } = string.Empty;
}
