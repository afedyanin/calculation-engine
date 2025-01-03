using CalcEngine.Application.Handlers.SimpleReport;
using CalcEngine.Client.Extensions;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
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

        var jobId = _backgroundJobs.EnqueueMediatorRequest(request1);

        var response = new GenerateReportResponse()
        {
            CorrelationId = request.CorrelationId,
            JobIds = [jobId]
        };

        return Task.FromResult(response);
    }


    public Task<GenerateReportResponse> Handle_old(
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

        var jobIds = _backgroundJobs.EnqueueSequence(request1, request2);

        var response = new GenerateReportResponse()
        {
            CorrelationId = request.CorrelationId,
            JobIds = jobIds,
        };

        _logger.LogInformation($"Jobs enqueued: {string.Join(',', jobIds)}");

        return Task.FromResult(response);
    }
}
