using CalculationEngine.Core.Model;
using CalculationEngine.Graphlib;
using CalculationEngine.Graphlib.Algos;
using MediatR;
using System.Text;

namespace CalculationEngine.Core.Extensions;

public static class CalculationGraphExtensions
{
    public static Vertex<CalculationUnit> AddCalculationVertex(this CalculationGraph graph, IRequest request)
    {
        var unit = new CalculationUnit
        {
            Id = Guid.NewGuid(),
            GraphId = graph.Id,
            Request = request
        };

        return graph.AddVertex(unit);
    }
    public static void Validate(this CalculationGraph graph)
    {
        if (graph.HasAnyCycle())
        {
            throw new InvalidOperationException("Graph has one or more cycles.");
        }
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
