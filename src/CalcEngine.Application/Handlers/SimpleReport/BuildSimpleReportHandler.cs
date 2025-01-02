using MediatR;

namespace CalcEngine.Application.Handlers.SimpleReport;
internal class BuildSimpleReportHandler : IRequestHandler<BuildSimpleReportRequest>
{
    public async Task Handle(BuildSimpleReportRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(2000);
    }
}
