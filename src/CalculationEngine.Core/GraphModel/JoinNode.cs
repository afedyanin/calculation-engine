using MediatR;

namespace CalculationEngine.Core.GraphModel;

public class JoinNode : NodeBase
{
    private readonly LinkedList<INode> _parents;

    public override IEnumerable<INode> Parents => _parents;

    public JoinNode(
        ICalculationGraph graph,
        IRequest request,
        int level,
        IEnumerable<INode> parents) : base(graph, request, level)
    {
        _parents = new LinkedList<INode>(parents);
    }

    public override string Enqueue()
    {
        return string.Empty;
    }
}
