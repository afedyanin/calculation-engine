using System.Text.Json;

namespace CalculationEngine.Graphlib.Tests;

[TestFixture(Category = "Unit")]
public class GraphTests
{
    [Test]
    public void CanCreateSimpleGraph()
    {
        var graph = GraphFactory.CreateSimpleGraph();

        graph.Vertices.ForEach(Console.WriteLine);
        graph.GetEdges().ForEach(Console.WriteLine);

        Assert.Multiple(() =>
        {
            Assert.That(graph[0, 1], Is.Not.Null);
            Assert.That(graph[0, 2], Is.Not.Null);
            Assert.That(graph[0, 3], Is.Null);
            Assert.That(graph[0, 4], Is.Null);
        });
    }
}
