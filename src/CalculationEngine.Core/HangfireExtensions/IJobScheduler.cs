using MediatR;

namespace CalculationEngine.Core.HangfireExtensions;
public interface IJobScheduler
{
    string Enqueue(IRequest request);

    string EnqueueAfter(string previousJobId, IRequest request);
}
