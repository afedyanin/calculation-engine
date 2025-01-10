namespace CalculationEngine.DataAccess.Entities;

internal class GraphDbo
{
    public Guid Id { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<VertexDbo> Vertices { get; set; } = [];
}
