using CalculationEngine.Core.Extensions;

namespace CalculationEngine.DataAccess.Tests.Repositories;

[TestFixture(Category = "Database", Explicit = true)]
public class CalculationGraphRepositoryTests : DbTestBase
{
    [Test]
    public async Task CanInsertSimpleGraph()
    {
        var graph = GraphStubs.CreateSimpleGraph();

        var isSaved = await CalculationGraphRepository.Insert(graph);

        Assert.That(isSaved, Is.True);

        Console.WriteLine($"id={graph.Id}");
        Console.WriteLine(graph.RenderVertices());
        Console.WriteLine(graph.RenderEdges());
    }

    [Test]
    public async Task CanGetSimpleGraph()
    {
        var graph = GraphStubs.CreateSimpleGraph();

        var isSaved = await CalculationGraphRepository.Insert(graph);
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

        var isSaved = await CalculationGraphRepository.Insert(graph);
        Assert.That(isSaved, Is.True);

        var isDeleted = await CalculationGraphRepository.Delete(graph.Id);
        Assert.That(isDeleted, Is.True);

        Console.WriteLine($"Deleted Id={graph.Id}");
    }
}
