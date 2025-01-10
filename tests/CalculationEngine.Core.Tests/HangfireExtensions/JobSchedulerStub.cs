using CalculationEngine.Core.HangfireExtensions;
using MediatR;

namespace CalculationEngine.Core.Tests.HangfireExtensions;

internal class JobSchedulerStub : IJobScheduler
{
    private int _nextJobId = 1;

    public string Enqueue(IRequest request)
        => _nextJobId++.ToString();

    public string EnqueueAfter(string previousJobId, IRequest request)
        => _nextJobId++.ToString();
}
