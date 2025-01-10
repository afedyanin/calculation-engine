using CalculationEngine.Core.Model;
using CalculationEngine.DataAccess.Entities;
using CalculationEngine.Graphlib;

namespace CalculationEngine.DataAccess.Converters;

internal static class VertexDboConverter
{
    public static async Task<List<Vertex<CalculationUnit>>> FromDbo(this IEnumerable<VertexDbo> vertexDbos, CalculationEngineDbContext context)
    {
        // Get calculation Units
        var unitIds = vertexDbos.Select(v => v.ValueId);
        var units = context.CalculationUnits.Where(u => unitIds.Contains(u.Id)).ToArray();

        // Create Vertices
        var vertices = new List<Vertex<CalculationUnit>>();

        foreach (var vertexDbo in vertexDbos)
        {
            vertices.Add(new Vertex<CalculationUnit>
            {
                Index = vertexDbo.Index,
                Value = units.Single(u => u.Id == vertexDbo.ValueId),
            });
        }

        // Update Children
        foreach (var vertexDbo in vertexDbos)
        {
            var vertice = vertices.Single(v => v.Index == vertexDbo.Index);
            vertice.Children = vertices.Where(v => vertexDbo.Children.Contains(v.Value.Id)).ToList();
        }

        return vertices;
    }

    public static List<VertexDbo> ToDbo(this List<Vertex<CalculationUnit>> vertices)
    {
        var dbos = new List<VertexDbo>();

        foreach (var vertex in vertices)
        {
            var vertexDbo = new VertexDbo()
            {
                Index = vertex.Index,
                ValueId = vertex.Value.Id,
                Children = vertex.Children.Select(v => v.Value.Id).ToList(),
            };

            dbos.Add(vertexDbo);
        }

        return dbos;
    }
}
