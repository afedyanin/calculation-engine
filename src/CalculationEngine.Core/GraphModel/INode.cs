using MediatR;

namespace CalculationEngine.Core.GraphModel;
public interface INode
{
    Guid Id { get; }

    IEnumerable<INode> Parents { get; }

    IEnumerable<INode> Children { get; }

    ICalculationGraph Graph { get; }

    IRequest Request { get; }

    string? JobId { get; }

    int Level { get; }

    INode AddChild(IRequest request);

    void Enqueue(IJobScheduler jobScheduler);

    string Render();
}
