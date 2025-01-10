using CalculationEngine.DataAccess.Entities;

namespace CalculationEngine.DataAccess.Tests;


[TestFixture(Category = "Database", Explicit = true)]
public class GraphDboTests : DbTestBase
{
    [Test]
    public void CanCreateGraph()
    {
        var vertices = GetVertices();
        var graph = new GraphDbo
        {
            Id = Guid.NewGuid(),
            Vertices = vertices,
            UpdatedAt = DateTime.UtcNow,
        };

        using var context = ContextFactory.CreateDbContext();
        context.Graphs.Add(graph);
        context.SaveChanges();

        Assert.Pass();
    }

    [Test]
    public void CanCreateAndRestoreGraph()
    {
        var vertices = GetVertices();
        var graph = new GraphDbo
        {
            Id = Guid.NewGuid(),
            Vertices = vertices,
            UpdatedAt = DateTime.UtcNow,
        };

        using var context = ContextFactory.CreateDbContext();
        context.Graphs.Add(graph);
        context.SaveChanges();

        var restored = context.Graphs.Where(x => x.Id == graph.Id).SingleOrDefault();

        Assert.That(restored, Is.Not.Null);
        Assert.That(restored.Vertices, Is.Not.Empty);
        Assert.That(restored.Vertices[0].Children, Is.Not.Empty);
    }

    private static List<VertexDbo> GetVertices()
    {
        var v1 = new VertexDbo() { Index = 1, ValueId = Guid.NewGuid() };
        var v2 = new VertexDbo() { Index = 2, ValueId = Guid.NewGuid() };
        var v3 = new VertexDbo() { Index = 3, ValueId = Guid.NewGuid() };
        var v4 = new VertexDbo() { Index = 4, ValueId = Guid.NewGuid() };
        var v5 = new VertexDbo() { Index = 5, ValueId = Guid.NewGuid() };

        v1.Children = [v2.ValueId, v3.ValueId];
        v2.Children = [v3.ValueId, v4.ValueId, v5.ValueId];
        v3.Children = [v4.ValueId, v5.ValueId];
        v4.Children = [v5.ValueId];

        var vertices = new List<VertexDbo>
        {
            v1, v2, v3, v4, v5
        };

        return vertices;
    }
}
