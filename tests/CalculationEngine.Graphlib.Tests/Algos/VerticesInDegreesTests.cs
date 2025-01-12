using System.Text;
using CalculationEngine.Graphlib.Algos;
using static CalculationEngine.Graphlib.Tests.GraphFactory;

namespace CalculationEngine.Graphlib.Tests.Algos;

[TestFixture(Category = "Unit")]
public class VerticesInDegreesTests
{
    [Test]
    public void CanSortSimpleGraph01()
    {
        var graph = new Graph<Item>();
        var v1 = graph.AddVertex(new Item(40));
        var v2 = graph.AddVertex(new Item(50));
        var v3 = graph.AddVertex(new Item(60));

        graph.AddEdge(v1, v2);
        graph.AddEdge(v2, v3);

        var inDegress = graph.InDegrees();

        var actual = string.Join(',', inDegress);
        var expected = string.Join(',', 0, 1, 1);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CanSortSimpleGraph02()
    {
        var graph = new Graph<Item>();
        var v1 = graph.AddVertex(new Item(40));
        var v2 = graph.AddVertex(new Item(50));
        var v3 = graph.AddVertex(new Item(60));

        graph.AddEdge(v1, v3);
        graph.AddEdge(v2, v3);

        var inDegress = graph.InDegrees();

        var actual = string.Join(',', inDegress);
        var expected = string.Join(',', 0, 0, 2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CanSortSimpleGraph03()
    {
        var graph = new Graph<Item>();
        var v1 = graph.AddVertex(new Item(40));
        var v2 = graph.AddVertex(new Item(50));
        var v3 = graph.AddVertex(new Item(60));
        var v4 = graph.AddVertex(new Item(70));
        var v5 = graph.AddVertex(new Item(80));
        var v6 = graph.AddVertex(new Item(90));

        graph.AddEdge(v1, v3);
        graph.AddEdge(v2, v3);
        graph.AddEdge(v3, v4);
        graph.AddEdge(v2, v5);
        graph.AddEdge(v4, v6);
        graph.AddEdge(v5, v6);

        var inDegress = graph.InDegrees();

        var actual = string.Join(',', inDegress);
        var expected = string.Join(',', 0, 0, 2, 1, 1, 2);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CanSortComplexGraph01()
    {
        var graph = CreateComplexGraph();

        var inDegress = graph.InDegrees();

        var actual = string.Join(',', inDegress);
        var expected = string.Join(',', 0, 0, 3, 1, 1, 0, 0, 2, 2, 2);
        Assert.That(actual, Is.EqualTo(expected));

        // topological

        var sorted = graph.TopologicalSort();
        actual = string.Join(',', sorted.Select(x => x.Index));

        var sb = new StringBuilder();

        foreach (var index in sorted.Select(x => x.Index))
        {
            sb.Append($"{inDegress[index]},");
        }

        Console.WriteLine($"actual={actual}");
        Console.WriteLine($"inDegress={sb}");
    }

    [Test]
    public void CanGetParentsForGraph()
    {
        var graph = CreateComplexGraph();
        var allParents = graph.GetParents();
        var sb2 = new StringBuilder();
        var sorted = graph.TopologicalSort();

        foreach (var index in sorted.Select(x => x.Index))
        {
            var parents = allParents[index];
            if (parents != null)
            {
                var indexes = parents.Select(x => x.Index);
                sb2.AppendLine($"[{string.Join(',', indexes)}] -> {index}");
            }
            else
            {
                sb2.AppendLine($"[] -> {index}");
            }
        }

        Console.WriteLine($"{sb2}");
    }
}
