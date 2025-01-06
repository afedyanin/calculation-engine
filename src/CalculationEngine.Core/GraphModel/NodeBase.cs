using MediatR;

namespace CalculationEngine.Core.GraphModel;

public abstract class NodeBase : INode
{
    private readonly LinkedList<INode> _children;

    public abstract IEnumerable<INode> Parents { get; }

    public IEnumerable<INode> Children => [.. _children];

    public ICalculationGraph Graph { get; private set; }

    public IRequest Request { get; private set; }

    public string? JobId { get; private set; }

    public int Level { get; private set; }

    protected NodeBase(
        ICalculationGraph graph,
        IRequest request,
        int level)
    {
        Graph = graph;
        Request = request;
        Level = level;
    }

    public virtual INode AddChild(IRequest request)
    {
        var childNode = new Node(
            Graph,
            request,
            Level++,
            this);

        _children.AddLast(childNode);
        return childNode;
    }

    public abstract string Enqueue();
}
