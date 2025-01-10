using CalculationEngine.Core.Handlers;
using CalculationEngine.Core.Model;
using CalculationEngine.Graphlib;
using CalculationEngine.Graphlib.Algos;
using System.Text;

namespace CalculationEngine.Core.Extensions;

public static class CalculationGraphExtensions
{
    public static Vertex<CalculationUnit> AddCalculationVertex(this CalculationGraph graph, ICalculationRequest request)
    {
        var unit = new CalculationUnit
        {
            Id = Guid.NewGuid(),
            GraphId = graph.Id,
            Request = request
        };

        return graph.AddVertex(unit);
    }

    public static void ThrowIfHasCycles(this CalculationGraph graph)
    {
        if (graph.HasAnyCycle())
        {
            throw new InvalidOperationException("Graph has one or more cycles.");
        }
    }

    public static bool HasInvalidInDegreeVertices(this CalculationGraph graph)
    {
        var inDegrees = graph.InDegrees();

        foreach (var vertex in graph.Vertices)
        {
            if (inDegrees[vertex.Index] > 1 &&
                vertex.Value.Request is not JobAwaitingRequest)
            {
                return true;
            }
        }

        return false;
    }

    public static string RenderVertices(this CalculationGraph graph)
    {
        var sb = new StringBuilder();
        graph.Vertices.ForEach(v => sb.AppendLine($"Index={v.Index} Id={v.Value.Id}"));
        return sb.ToString();
    }

    public static string RenderEdges(this CalculationGraph graph)
    {
        var sb = new StringBuilder();
        var edges = graph.GetEdges();
        edges.ForEach(e => sb.AppendLine(e.ToString()));
        return sb.ToString();
    }
}
