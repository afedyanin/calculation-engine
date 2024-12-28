using Hangfire;
using HfDemo.Contracts.Services;
using MediatR;

namespace HfDemo.Contracts.Internals;

internal class JobEnqueueService : IJobEnqueueService
{
    private readonly IBackgroundJobClient _backgroundJobClient;

    public JobEnqueueService(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }

    public string Enqueue(ICalculationRequest request, CancellationToken cancellationToken)
    {
        var jobId = _backgroundJobClient.Enqueue<IMediator>(mediator => mediator.Send(request, cancellationToken));
        var childJobId = _backgroundJobClient.ContinueJobWith<IMediator>(jobId, mediator => mediator.Send(request, cancellationToken));
        return jobId;
    }
}
