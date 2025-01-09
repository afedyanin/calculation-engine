using System.Text;
using CalculationEngine.Core.Handlers;
using MediatR;

namespace CalculationEngine.Core.GraphModel;

public class JoinNode : NodeBase
{
    private readonly LinkedList<INode> _parents;

    private readonly TimeSpan _poolingInterval;

    public string? AwaitingJobId { get; private set; }

    public override IEnumerable<INode> Parents => _parents;

    public JoinNode(
        ICalculationGraph graph,
        IRequest request,
        int level,
        IEnumerable<INode> parents,
        TimeSpan? poolingInterval = null) : base(graph, request, level)
    {
        _parents = new LinkedList<INode>(parents);
        _poolingInterval = poolingInterval ?? TimeSpan.FromSeconds(10);
    }

    public override void Enqueue(IJobScheduler jobScheduler)
    {
        var ids = GetParentJobIds();
        var jobAwatingRequest = new JobAwaitingRequest
        {
            JobIds = ids,
            CalculationUnitId = Graph.Id,
            PoolingInterval = _poolingInterval
        };

        AwaitingJobId = jobScheduler.Enqueue(jobAwatingRequest);
        JobId = jobScheduler.EnqueueAfter(AwaitingJobId, Request);
    }

    public string[] GetParentJobIds()
    {
        var ids = new List<string>(_parents.Count);

        foreach (var item in _parents)
        {
            if (string.IsNullOrEmpty(item.JobId))
            {
                throw new InvalidOperationException();
            }

            ids.Add(item.JobId);
        }

        return [.. ids];
    }

    public override string Render()
    {
        if (!Enqueued)
        {
            return string.Empty;
        }

        var sb = new StringBuilder();

        var ident = new string('\t', Level);
        var parentIds = string.Join(',', GetParentJobIds());

        sb.Append($"{ident}J({parentIds}) -> A({AwaitingJobId}) -> N({JobId}) ");

        if (Children.Any())
        {
            var ids = string.Join(',', GetChildIds());
            sb.Append($"-> ({ids})");
        }

        sb.AppendLine();

        foreach (var node in Children)
        {
            sb.Append(node.Render());
        }

        return sb.ToString();
    }
}
