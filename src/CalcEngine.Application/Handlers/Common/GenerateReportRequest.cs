using MediatR;

namespace CalcEngine.Application.Handlers.Common;
public class GenerateReportRequest : IRequest<GenerateReportResponse>
{
    public Guid CorrelationId { get; set; }
}
