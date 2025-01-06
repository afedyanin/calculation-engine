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

    public override string Enqueue()
    {
        return string.Empty;
    }
}
