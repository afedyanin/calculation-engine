using MediatR;

namespace CalculationEngine.Core.GraphModel;
public interface ICalculationGraph
{
    Guid Id { get; }

    INode AddRoot(IRequest request);

    INode Join(INode[] nodes, IRequest request);

    void Enqueue(IJobScheduler jobScheduler);

    string Render();
}
