using MediatR;

namespace CalculationEngine.AppDemo.SimpleReport;

internal class PrepareSimpleReportHandler : IRequestHandler<PrepareSimpleReportRequest>
{
    public async Task Handle(PrepareSimpleReportRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(2000);
    }
}
