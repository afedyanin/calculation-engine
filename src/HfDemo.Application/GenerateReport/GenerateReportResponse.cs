using HfDemo.Application.Domain;

namespace HfDemo.Application.GenerateReport;

public class GenerateReportResponse
{
    public Guid ReportId { get; set; }

    public ReportStatus Status { get; set; }
}
