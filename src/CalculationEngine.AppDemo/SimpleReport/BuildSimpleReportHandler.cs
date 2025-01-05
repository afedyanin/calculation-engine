using MediatR;

namespace CalculationEngine.AppDemo.SimpleReport;
internal class BuildSimpleReportHandler : IRequestHandler<BuildSimpleReportRequest>
{
    public async Task Handle(BuildSimpleReportRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(2000);
    }
}
