namespace CalculationEngine.DataAccess.Entities;

internal class VertexDbo
{
    public int Index { get; set; }

    public Guid ValueId { get; set; }

    public List<Guid> Children { get; set; } = [];
}
