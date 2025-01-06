using MediatR;

namespace CalculationEngine.Core.GraphModel;
public interface IJobScheduler
{
    string Enqueue(IRequest request);

    string EnqueueAfter(string previousJobId, IRequest request);
}
