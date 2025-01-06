using MediatR;

namespace CalculationEngine.Core.GraphModel;

public abstract class NodeBase : INode
{
    private readonly LinkedList<INode> _children;

    public abstract IEnumerable<INode> Parents { get; }

    public IEnumerable<INode> Children => [.. _children];

    public ICalculationGraph Graph { get; private set; }

    public IRequest Request { get; private set; }

    public int Level { get; private set; }

    public string? JobId { get; protected set; }

    protected bool Enqueued => !string.IsNullOrEmpty(JobId);

    protected NodeBase(
        ICalculationGraph graph,
        IRequest request,
        int level)
    {
        Graph = graph;
        Request = request;
        Level = level;
        _children = new LinkedList<INode>();
    }

    public virtual INode AddChild(IRequest request)
    {
        var childNode = new Node(
            Graph,
            request,
            Level + 1,
            this);

        _children.AddLast(childNode);
        return childNode;
    }

    public abstract void Enqueue(IJobScheduler jobScheduler);

    public abstract string Render();

    protected string[] GetChildIds() =>
        _children.Select(node => node.JobId).ToArray();
}
