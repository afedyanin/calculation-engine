using MediatR;

namespace CalculationEngine.Core.GraphModel;
public class Node : NodeBase
{
    private readonly INode _parentNode;

    public override IEnumerable<INode> Parents => [_parentNode];

    public Node(
        ICalculationGraph graph,
        IRequest request,
        int level,
        INode parent) : base(graph, request, level)
    {
        _parentNode = parent;
    }

    public override string Enqueue()
    {
        return string.Empty;
    }
}
