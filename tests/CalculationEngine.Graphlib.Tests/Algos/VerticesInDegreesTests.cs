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

}
