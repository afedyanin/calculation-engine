using CalculationEngine.Core.Model;
using CalculationEngine.DataAccess.Entities;

namespace CalculationEngine.DataAccess.Converters;
internal static class GraphDboConverter
{
    public static async Task<CalculationGraph> FromDbo(this GraphDbo graphDbo, CalculationEngineDbContext context)
    {
        var vertices = await graphDbo.Vertices.FromDbo(context);

        var graph = new CalculationGraph()
        {
            Id = graphDbo.Id,
            Vertices = vertices,
        };

        return graph;
    }

    public static GraphDbo ToDbo(this CalculationGraph graph)
    {
        var graphDbo = new GraphDbo()
        {
            Id = graph.Id,
            Vertices = graph.Vertices.ToDbo(),
            UpdatedAt = DateTime.UtcNow,
        };

        return graphDbo;
    }
}
