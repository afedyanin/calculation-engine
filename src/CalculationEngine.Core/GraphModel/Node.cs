using System.Text;
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

    public override void Enqueue(IJobScheduler jobScheduler)
    {
        if (string.IsNullOrEmpty(_parentNode.JobId))
        {
            throw new InvalidOperationException("Cannot enqueue child job with no parent jobId.");
        }

        JobId = jobScheduler.EnqueueAfter(_parentNode.JobId, Request);

        foreach (var node in Children)
        {
            node.Enqueue(jobScheduler);
        }
    }
    public override string Render()
    {
        if (!Enqueued)
        {
            return string.Empty;
        }

        var sb = new StringBuilder();
        var ident = new string('\t', Level);

        sb.Append($"{ident}N({JobId}) ");

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
