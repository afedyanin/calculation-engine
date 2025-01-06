using MediatR;

namespace CalculationEngine.Core.GraphModel;
public class CalculationGraph : ICalculationGraph
{
    private readonly LinkedList<INode> _rootNodes;

    public Guid Id { get; }

    public CalculationGraph()
    {
        Id = Guid.NewGuid();
        _rootNodes = new LinkedList<INode>();
    }

    public INode AddRoot(IRequest request)
    {
        var rootNode = new RootNode(this, request);
        _rootNodes.AddLast(rootNode);

        return rootNode;
    }

    public INode Join(INode[] nodes, IRequest request)
    {
        // TODO: Validate nodes belong this graph
        var level = GetMaxLevel(nodes);
        var joinNode = new JoinNode(this, request, level++, nodes);

        return joinNode;
    }
    public void Enqueue(IJobScheduler jobScheduler)
    {
        throw new NotImplementedException();
    }

    public string Render()
    {
        throw new NotImplementedException();
    }

    private int GetMaxLevel(IEnumerable<INode> nodes)
        => nodes == null ? 0 : nodes.Max(x => x.Level);
}
