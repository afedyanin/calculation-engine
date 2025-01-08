using static CalculationEngine.Graphlib.Tests.GraphFactory;

namespace CalculationEngine.Graphlib.Tests;

[TestFixture(Category = "Unit")]
public class CycleDetectionTests
{
    [Test]
    public void SimpleGraphHasNoCycles()
    {
        var graph = GraphFactory.CreateSimpleGraph();
        var hasCycle = graph.HasAnyCycle();

        Assert.That(hasCycle, Is.False);
    }

    [Test]
    public void CyclicGraphHasCycles()
    {
        var graph = GraphFactory.CreateCyclicGraph();
        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.True);
    }

    [Test]
    public void CyclicGraphWithRemovedCyclesHasNoCycles()
    {
        var graph = GraphFactory.CreateCyclicGraph();
        var v8 = graph.Vertices[7];
        var v4 = graph.Vertices[3];
        var v6 = graph.Vertices[5];

        graph.RemoveEdge(v8, v4);
        graph.RemoveEdge(v6, v4);

        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.False);
    }

    [Test]
    public void CyclicGraphWithRemovedOneCycleHasCycles01()
    {
        var graph = GraphFactory.CreateCyclicGraph();
        var v8 = graph.Vertices[7];
        var v4 = graph.Vertices[3];
        var v6 = graph.Vertices[5];

        //graph.RemoveEdge(v8, v4);
        graph.RemoveEdge(v6, v4);

        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.True);
    }

    [Test]
    public void CyclicGraphWithRemovedOneCycleHasCycles02()
    {
        var graph = GraphFactory.CreateCyclicGraph();
        var v8 = graph.Vertices[7];
        var v4 = graph.Vertices[3];
        var v6 = graph.Vertices[5];

        graph.RemoveEdge(v8, v4);
        //graph.RemoveEdge(v6, v4);

        graph.GetEdges().ForEach(x => Console.WriteLine($"{x.From.Index + 1} -> {x.To.Index + 1}"));

        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.True);
    }

    [Test]
    public void CircleGraphHasCycles()
    {
        var graph = GraphFactory.CreateCircleGraph();

        graph.GetEdges().ForEach(Console.WriteLine);

        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.True);
    }

    [Test]
    public void CircleGraphWithoutOneEdgeHasNoCycles01()
    {
        var graph = GraphFactory.CreateCircleGraph();
        var v4 = graph.Vertices[3];
        var v1 = graph.Vertices[0];
        graph.RemoveEdge(v4, v1);

        graph.GetEdges().ForEach(Console.WriteLine);

        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.False);
    }

    [Test]
    public void CircleGraphWithoutOneEdgeHasNoCycles02()
    {
        var graph = GraphFactory.CreateCircleGraph();
        var v2 = graph.Vertices[1];
        var v3 = graph.Vertices[2];
        graph.RemoveEdge(v2, v3);

        graph.GetEdges().ForEach(Console.WriteLine);

        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.False);
    }

    [Test]
    public void CircleGraphWithAdditionalEdgeHasCycles01()
    {
        var graph = GraphFactory.CreateCircleGraph();
        var v2 = graph.Vertices[1];
        var v4 = graph.Vertices[3];
        graph.AddEdge(v4, v2);

        graph.GetEdges().ForEach(Console.WriteLine);

        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.True);
    }

    [Test]
    public void CircleGraphWithAdditionalEdgeHasCycles02()
    {
        var graph = GraphFactory.CreateCircleGraph();
        var v1 = graph.Vertices[0];
        var v2 = graph.Vertices[1];
        var v4 = graph.Vertices[3];
        graph.AddEdge(v4, v2);
        graph.RemoveEdge(v4, v1);

        graph.GetEdges().ForEach(Console.WriteLine);

        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.True);
    }

    [Test]
    public void SpecialCase01()
    {
        var graph = new Graph<Item>();
        var v1 = graph.AddVertex(new Item(40));

        graph.AddEdge(v1, v1);

        graph.GetEdges().ForEach(x => Console.WriteLine($"{x.From.Index + 1} -> {x.To.Index + 1}"));

        var hasCycle = graph.HasAnyCycle();

        Assert.That(hasCycle, Is.True);
    }

    [Test]
    public void SpecialCase02()
    {
        var graph = new Graph<Item>();
        var v1 = graph.AddVertex(new Item(40));
        var v2 = graph.AddVertex(new Item(50));

        graph.AddEdge(v1, v2);
        graph.AddEdge(v2, v1);

        graph.GetEdges().ForEach(x => Console.WriteLine($"{x.From.Index + 1} -> {x.To.Index + 1}"));

        var hasCycle = graph.HasAnyCycle();

        Assert.That(hasCycle, Is.True);
    }

    [Test]
    public void SpecialCase03()
    {
        var graph = new Graph<Item>();
        var v1 = graph.AddVertex(new Item(40));
        var v2 = graph.AddVertex(new Item(50));
        var v3 = graph.AddVertex(new Item(60));

        graph.AddEdge(v1, v2);
        graph.AddEdge(v2, v3);
        graph.AddEdge(v2, v1);

        graph.GetEdges().ForEach(x => Console.WriteLine($"{x.From.Index + 1} -> {x.To.Index + 1}"));

        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.True);
    }
}
