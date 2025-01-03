using CalcEngine.Application.Handlers.SimpleReport;
using CalcEngine.Client;
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

        var request1 = new PrepareSimpleReportRequest()
        {
            CorrelationId = request.CorrelationId,
        };

        var request2 = new BuildSimpleReportRequest()
        {
            CorrelationId = request.CorrelationId,
        };

        CalcJobRequest[] requests = [request1, request2];

        var jobIds = EnqueueSequence(requests, cancellationToken);

        //var jobIds = _backgroundJobs.EnqueueSequence(requests, cancellationToken);
        //var jobIds = new[] { jobId1, jobId2 };

        var response = new GenerateReportResponse()
        {
            CorrelationId = request.CorrelationId,
            JobIds = jobIds,
        };

        _logger.LogInformation($"Jobs enqueued: {string.Join(',', jobIds)}");

        return Task.FromResult(response);
    }
    private IEnumerable<string> EnqueueSequence(
        IEnumerable<CalcJobRequest> requests,
        CancellationToken cancellationToken = default)
    {
        var jobIds = new List<string>();
        var jobId = string.Empty;

        foreach (var request in requests)
        {
            // first job
            if (string.IsNullOrEmpty(jobId))
            {
                jobId = _backgroundJobs.Enqueue<IMediator>(m => m.Send(request, cancellationToken));
                jobIds.Add(jobId);
                continue;
            }
            jobId = _backgroundJobs.ContinueJobWith<IMediator>(jobId, m => m.Send(request, cancellationToken));
            jobIds.Add(jobId);
        }

        return jobIds;
    }

}
