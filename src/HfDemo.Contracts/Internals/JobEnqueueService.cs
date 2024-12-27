using Hangfire;
using HfDemo.Contracts.Services;
using MediatR;

namespace HfDemo.Contracts.Internals;

internal class JobEnqueueService : IJobEnqueueService
{
    private readonly IMediator _mediator;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public JobEnqueueService(IMediator mediator, IBackgroundJobClient backgroundJobClient)
    {

        _mediator = mediator;
        _backgroundJobClient = backgroundJobClient;
    }

    public string Enqueue(ICalculationRequest request, CancellationToken cancellationToken)
    {
        var jobId = _backgroundJobClient.Enqueue(() => _mediator.Send(request, cancellationToken));
        return jobId;
    }
}
