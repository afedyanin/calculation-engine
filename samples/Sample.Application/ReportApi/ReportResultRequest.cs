using MediatR;

namespace Sample.Application.ReportApi;

public class ReportResultRequest : IRequest<ReportResultResponse>
{
    public Guid ReportResultCalculationUnitId { get; set; }
}
