using CalculationEngine.Core.Extensions;
using CalculationEngine.Core.Model;

namespace CalculationEngine.DataAccess.Tests.Repositories;

[TestFixture(Category = "Database", Explicit = true)]
public class CalculationGraphRepositoryTests : DbTestBase
{
    [Test]
    public async Task CanSaveSimpleGraph()
    {
        var graph = GraphStubs.CreateSimpleGraph();

        var isSaved = await CalculationGraphRepository.Save(graph);

        Assert.That(isSaved, Is.True);

        Console.WriteLine($"id={graph.Id}");
        Console.WriteLine(graph.RenderVertices());
        Console.WriteLine(graph.RenderEdges());
    }

    [Test]
    public async Task CanLoadSimpleGraph()
    {
        var graph = GraphStubs.CreateSimpleGraph();

        var isSaved = await CalculationGraphRepository.Save(graph);
        Assert.That(isSaved, Is.True);

        var loaded = await CalculationGraphRepository.GetById(graph.Id);
        Assert.That(loaded, Is.Not.Null);
        Assert.That(loaded.Vertices, Has.Count.EqualTo(graph.Vertices.Count));

        var graphString = $"{graph.RenderVertices()}{graph.RenderEdges()}";
        var loadedString = $"{loaded.RenderVertices()}{loaded.RenderEdges()}";
        Assert.That(loadedString, Is.EqualTo(graphString));
    }

    [Test]
    public async Task CanDeleteSimpleGraph()
    {
        var graph = GraphStubs.CreateSimpleGraph();

        var isSaved = await CalculationGraphRepository.Save(graph);
        Assert.That(isSaved, Is.True);

        var isDeleted = await CalculationGraphRepository.Delete(graph.Id);
        Assert.That(isDeleted, Is.True);

        Console.WriteLine($"Deleted Id={graph.Id}");
    }

    [Test]
    public async Task CanDeleteSimpleGraphWithResultSet()
    {
        var graph = GraphStubs.CreateSimpleGraph();

        var isSaved = await CalculationGraphRepository.Save(graph);
        Assert.That(isSaved, Is.True);

        var v2 = graph.Vertices[1];
        var record = new SimpleRecord(Guid.NewGuid(), "Just Record", DateTime.UtcNow, 1234.56M);

        var result = new CalculationResultItem()
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = v2.Value.Id,
            Name = "Name",
            Content = record,
        };

        v2.Value.Results.Add(result);

        var isUnitSaved = await CalculationUnitRepository.Update(v2.Value);
        Assert.That(isUnitSaved, Is.True);

        var isDeleted = await CalculationGraphRepository.Delete(graph.Id);
        Assert.That(isDeleted, Is.True);

        Console.WriteLine($"Deleted Id={graph.Id} UnitId={v2.Value.Id} ResultId={result.Id}");
    }
}
