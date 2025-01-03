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

        var jobIds = new RequestSequenceBuilder(_backgroundJobs)
            .WithRequest(new PrepareSimpleReportRequest()
            {
                CorrelationId = request.CorrelationId,
            })
            .WithRequest(new PrepareSimpleReportRequest()
            {
                CorrelationId = request.CorrelationId,
            })
            .WithRequest(new BuildSimpleReportRequest()
            {
                CorrelationId = request.CorrelationId,
            })
            .WithRequest(new BuildSimpleReportRequest()
            {
                CorrelationId = request.CorrelationId,
            })
            .GetJobIds();

        var response = new GenerateReportResponse()
        {
            CorrelationId = request.CorrelationId,
            JobIds = jobIds,
        };

        _logger.LogInformation($"Jobs enqueued: {string.Join(',', jobIds)}");

        return Task.FromResult(response);
    }
}
