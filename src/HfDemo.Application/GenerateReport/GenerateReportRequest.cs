using MediatR;

namespace HfDemo.Application.GenerateReport;

public class GenerateReportRequest : IRequest
{
    public Guid ReportId { get; set; }

    public DateTime AsOfDate { get; set; }

    public string[] AdditionalData { get; set; } = Array.Empty<string>();
}
