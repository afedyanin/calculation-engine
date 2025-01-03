using CalcEngine.Application.Handlers.SimpleReport;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
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

        var job = CreateJob(request1, cancellationToken);
        var jobid = _backgroundJobs.Create(job, new EnqueuedState());


        //IRequest[] requests = [request1, request2];
        //var jobIds = EnqueueSequence(requests, cancellationToken);

        //var jobId1 = Enqueue(request1, cancellationToken);
        //var jobId2 = ContinueJobWith(jobId1, request2, cancellationToken);
        //var jobIds = new[] { jobId1, jobId2 };

        var response = new GenerateReportResponse()
        {
            CorrelationId = request.CorrelationId,
            //JobIds = jobIds,
        };

        // _logger.LogInformation($"Jobs enqueued: {string.Join(',', jobIds)}");
        _logger.LogInformation($"Job enqueued: {jobid}");

        return Task.FromResult(response);
    }
    private IEnumerable<string> EnqueueSequence(
        IRequest[] requests,
        CancellationToken cancellationToken = default)
    {
        var jobIds = new List<string>();
        var jobId = string.Empty;

        foreach (var request in requests)
        {
            // first job
            if (string.IsNullOrEmpty(jobId))
            {
                jobId = Enqueue(request, cancellationToken);
                jobIds.Add(jobId);
                continue;
            }

            jobId = ContinueJobWith(jobId, request, cancellationToken);
            jobIds.Add(jobId);
        }

        return jobIds;
    }

    private string Enqueue<T>(T request, CancellationToken token) where T : IRequest
    {
        var job = CreateJob(request, token);
        var jobId = _backgroundJobs.Create(job, new EnqueuedState());
        return jobId;
    }

    private string ContinueJobWith<T>(string parentId, T request, CancellationToken token) where T : IRequest
    {
        var job = CreateJob(request, token);
        var jobId = _backgroundJobs.Create(job, new AwaitingState(parentId));
        return jobId;
    }

    private Job CreateJob<T>(T request, CancellationToken token) where T : IRequest
    {
        var type = typeof(Mediator);
        var methodInfo = type.GetMethod("Send", [typeof(IRequest), typeof(CancellationToken)]);
        _logger.LogInformation($"request type: {request.GetType().Name}");
        return new Job(type, methodInfo, request, token);
    }
}
