using MediatR;

namespace HfDemo.Application.GetReportStatus;

public class GetReportStatusRequest : IRequest<GetReportStatusResponse>
{
    public Guid ReportId { get; set; }
}
