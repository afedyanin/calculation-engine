using CalcEngine.Application.Handlers.SimpleReport;
using CalcEngine.Client.Extensions;
using Hangfire;
using MediatR;

namespace CalcEngine.Application.Handlers.Common;
internal class GenerateReportHandler : IRequestHandler<GenerateReportRequest, GenerateReportResponse>
{
    private readonly IBackgroundJobClient _backgroundJobs;

    public GenerateReportHandler(IBackgroundJobClient backgroundJob)
    {
        _backgroundJobs = backgroundJob;
    }

    public Task<GenerateReportResponse> Handle(
        GenerateReportRequest request,
        CancellationToken cancellationToken)
    {
        var requests = new List<IRequest>
        {
            new PrepareSimpleReportRequest(),
            new BuildSimpleReportRequest()
        };

        var jobIds = _backgroundJobs.EnqueueSequence(requests, cancellationToken);

        var response = new GenerateReportResponse()
        {
            JobIds = jobIds,
        };

        return Task.FromResult(response);
    }
}
