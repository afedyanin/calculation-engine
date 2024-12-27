using Hangfire;
using MediatR;

namespace HfDemo.Contracts.Internals;

internal class CalculationJobEnqueueService : ICalculationJobEnqueueService
{
    private readonly IMediator _mediator;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public CalculationJobEnqueueService(IMediator mediator, IBackgroundJobClient backgroundJobClient)
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
