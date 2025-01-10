using System.Text;
using CalculationEngine.Graphlib.Algos;
using static CalculationEngine.Graphlib.Tests.GraphFactory;

namespace CalculationEngine.Graphlib.Tests.Algos;

[TestFixture(Category = "Unit")]
public class TopologicalSortingTests
{
    [Test]
    public void CanSortSimpleGraph()
    {
        var graph = new Graph<Item>();
        var v1 = graph.AddVertex(new Item(40));
        var v2 = graph.AddVertex(new Item(50));
        var v3 = graph.AddVertex(new Item(60));

        graph.AddEdge(v3, v2);
        graph.AddEdge(v2, v1);

        var sorted = graph.TopologicalSort();

        var actual = string.Join(',', sorted.Select(x => x.Index + 1));
        var expected = string.Join(',', 3, 2, 1);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    // https://www.geeksforgeeks.org/topological-sorting/
    public void CanSortGraph01()
    {
        var graph = new Graph<Item>();
        var v0 = graph.AddVertex(new Item(0));
        var v1 = graph.AddVertex(new Item(10));
        var v2 = graph.AddVertex(new Item(20));
        var v3 = graph.AddVertex(new Item(30));
        var v4 = graph.AddVertex(new Item(40));
        var v5 = graph.AddVertex(new Item(50));

        graph.AddEdge(v3, v1);
        graph.AddEdge(v2, v3);
        graph.AddEdge(v5, v2);
        graph.AddEdge(v4, v1);
        graph.AddEdge(v5, v0);
        graph.AddEdge(v4, v0);

        var sorted = graph.TopologicalSort();

        var actual = string.Join(',', sorted.Select(x => x.Index));
        var expected = string.Join(',', 5, 4, 2, 3, 1, 0);

        Assert.That(actual, Is.EqualTo(expected));

        var inDegress = graph.InDegrees();
        var sb = new StringBuilder();

        foreach (var index in sorted.Select(x => x.Index))
        {
            sb.Append($"{inDegress[index]},");
        }

        Console.WriteLine($"actual={actual}");
        Console.WriteLine($"inDegress={sb}");
    }
}
