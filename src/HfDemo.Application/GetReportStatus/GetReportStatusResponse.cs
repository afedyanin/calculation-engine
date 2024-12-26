using HfDemo.Application.Domain;

namespace HfDemo.Application.GetReportStatus;

public class GetReportStatusResponse
{
    public static readonly GetReportStatusResponse EmptyStatus = new()
    {
        ReportId = Guid.Empty,
        Status = ReportStatus.Unknown,
    };

    public Guid ReportId { get; set; }

    public ReportStatus Status { get; set; }
}
