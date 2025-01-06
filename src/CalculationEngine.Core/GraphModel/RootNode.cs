using MediatR;

namespace CalculationEngine.Core.GraphModel;

public class RootNode : NodeBase
{
    public RootNode(
        ICalculationGraph graph,
        IRequest request) : base(graph, request, 0)
    {
    }

    public override IEnumerable<INode> Parents => [];

    public override void Enqueue(IJobScheduler jobScheduler)
    {
        JobId = jobScheduler.Enqueue(Request);

        foreach (var node in Children)
        {
            node.Enqueue(jobScheduler);
        }
    }
}
