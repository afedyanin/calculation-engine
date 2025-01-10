namespace CalculationEngine.DataAccess.Entities;

internal class GraphDbo
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<VertexDbo> Vertices { get; set; } = [];
}
