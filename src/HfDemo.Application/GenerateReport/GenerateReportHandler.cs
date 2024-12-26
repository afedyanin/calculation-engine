using MediatR;

namespace HfDemo.Application.GenerateReport;

internal class GenerateReportHandler : IRequestHandler<GenerateReportRequest, GenerateReportResponse>
{
    public Task<GenerateReportResponse> Handle(GenerateReportRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
