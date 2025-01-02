using MediatR;

namespace CalcEngine.Application.Handlers.SimpleReport;

internal class PrepareSimpleReportHandler : IRequestHandler<PrepareSimpleReportRequest>
{
    public async Task Handle(PrepareSimpleReportRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(2000);
    }
}
