using CalculationEngine.Core.GraphModel;
using MediatR;

namespace CalculationEngine.Core.HangfireExtensions;
internal class HangfireJobScheduler : IJobScheduler
{
    public string Enqueue(IRequest request)
    {
        throw new NotImplementedException();
    }

    public string EnqueueAfter(string previousJobId, IRequest request)
    {
        throw new NotImplementedException();
    }
}
