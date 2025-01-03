using CalcEngine.Application.Handlers.SimpleReport;
using CalcEngine.Client.Extensions;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalcEngine.Application.Handlers.Common;
internal class GenerateReportHandler : IRequestHandler<GenerateReportRequest, GenerateReportResponse>
{
    private readonly IBackgroundJobClient _backgroundJobs;
    private readonly ILogger<GenerateReportHandler> _logger;

    public GenerateReportHandler(
        IBackgroundJobClient backgroundJob,
        ILogger<GenerateReportHandler> logger)
    {
        _backgroundJobs = backgroundJob;
        _logger = logger;
    }

    public Task<GenerateReportResponse> Handle(
        GenerateReportRequest request,
        CancellationToken cancellationToken)
    {

        _logger.LogInformation($"Handler called with params: CorrelationId={request.CorrelationId}");

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

        _logger.LogInformation($"Jobs enqueued: {string.Join(',', jobIds)}");

        return Task.FromResult(response);
    }
}
