using System.Text;
using CalculationEngine.Graphlib;
using CalculationEngine.Graphlib.Algos;
using MediatR;

namespace CalculationEngine.Core.Model;

public class CalculationGraph : Graph<CalculationUnit>
{
    public Guid Id { get; set; }

    public Vertex<CalculationUnit> AddVertex(IRequest request)
    {
        var unit = new CalculationUnit
        {
            Id = Guid.NewGuid(),
            GraphId = Id,
            Request = request
        };

        return AddVertex(unit);
    }

    public string RenderVertices()
    {
        var sb = new StringBuilder();
        Vertices.ForEach(v => sb.AppendLine($"Index={v.Index} Id={v.Value.Id}"));
        return sb.ToString();
    }

    public string RenderEdges()
    {
        var sb = new StringBuilder();
        var edges = GetEdges();
        edges.ForEach(e => sb.AppendLine(e.ToString()));
        return sb.ToString();
    }

    public void Validate()
    {
        if (this.HasAnyCycle())
        {
            throw new InvalidOperationException("Graph has one or more cycles.");
        }
    }
}

