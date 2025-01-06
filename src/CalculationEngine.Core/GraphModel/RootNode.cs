using System.Text;
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

    public override string Render()
    {
        if (!Enqueued)
        {
            return string.Empty;
        }

        var sb = new StringBuilder();

        sb.Append($"R({JobId}) ");

        if (Children.Any())
        {
            sb.Append($"-> ");
        }

        foreach (var node in Children)
        {
            sb.Append(node.Render());
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
