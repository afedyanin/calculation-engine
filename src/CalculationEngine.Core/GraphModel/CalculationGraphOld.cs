using System.Text;
using MediatR;

namespace CalculationEngine.Core.GraphModel;
public class CalculationGraphOld : ICalculationGraph
{
    private readonly LinkedList<INode> _rootNodes;
    private readonly LinkedList<INode> _joinNodes;

    public Guid Id { get; }

    public CalculationGraphOld()
    {
        Id = Guid.NewGuid();
        _rootNodes = new LinkedList<INode>();
        _joinNodes = new LinkedList<INode>();
    }

    public INode AddRoot(IRequest request)
    {
        var rootNode = new RootNode(this, request);
        _rootNodes.AddLast(rootNode);

        return rootNode;
    }

    public INode Join(INode[] nodes, IRequest request)
    {
        if (!OwnNodes(nodes))
        {
            throw new InvalidOperationException("Cannot join nodes with different graphs.");
        }

        var level = GetMaxLevel(nodes);
        var joinNode = new JoinNode(this, request, level++, nodes);
        _joinNodes.AddLast(joinNode);

        return joinNode;
    }
    public void Enqueue(IJobScheduler jobScheduler)
    {
        foreach (var node in _rootNodes)
        {
            node.Enqueue(jobScheduler);
        }
        foreach (var node in _joinNodes)
        {
            node.Enqueue(jobScheduler);
        }
    }

    public string Render()
    {
        var sb = new StringBuilder();

        foreach (var node in _rootNodes)
        {
            sb.AppendLine(node.Render());
        }

        foreach (var node in _joinNodes)
        {
            sb.AppendLine(node.Render());
        }

        return sb.ToString();
    }

    private int GetMaxLevel(IEnumerable<INode> nodes)
        => nodes == null ? 0 : nodes.Max(x => x.Level);

    private bool OwnNodes(INode[] nodes)
    {
        foreach (var node in nodes)
        {
            if (node.Graph.Id != Id)
            {
                return false;
            }
        }
        return true;
    }
}
