using CalculationEngine.Core.GraphModel;
using MediatR;

namespace CalculationEngine.Core.Tests.GraphModel;

internal class JobSchedulerStub : IJobScheduler
{
    private int _nextJobId = 1;

    public string Enqueue(IRequest request)
        => _nextJobId++.ToString();

    public string EnqueueAfter(string previousJobId, IRequest request)
        => _nextJobId++.ToString();
}
