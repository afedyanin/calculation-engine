namespace CalculationEngine.Graphlib.Tests;

[TestFixture(Category = "Unit")]
public class GraphTests
{
    private record Item(int Num);

    [Test]
    public void CanCreateSimpleGraph()
    {

        var graph = CreateSimpleGraph();

        Assert.Multiple(() =>
        {
            Assert.That(graph[0, 1], Is.Not.Null);
            Assert.That(graph[0, 3], Is.Not.Null);
            Assert.That(graph[0, 4], Is.Null);
        });

        foreach (var v in graph.Vertices)
        {
            Console.WriteLine(v);
        }

        foreach (var edge in graph.GetEdges())
        {
            Console.WriteLine(edge);
        }
    }

    [Test]
    public void CanUseDFS()
    {
        var graph = CreateSimpleGraph();
        var vertices = graph.DFS();
        vertices.ForEach(Console.WriteLine);

        Assert.Pass();
    }

    [Test]
    public void CanUseBFS()
    {
        var graph = CreateSimpleGraph();
        var vertices = graph.BFS();
        vertices.ForEach(Console.WriteLine);

        Assert.Pass();
    }

    private static Graph<Item> CreateSimpleGraph()
    {
        var graph = new Graph<Item>();

        var v1 = graph.AddVertex(new Item(10));
        var v2 = graph.AddVertex(new Item(20));
        var v3 = graph.AddVertex(new Item(30));
        var v4 = graph.AddVertex(new Item(40));
        var v5 = graph.AddVertex(new Item(50));
        var v6 = graph.AddVertex(new Item(60));
        var v7 = graph.AddVertex(new Item(70));
        var v8 = graph.AddVertex(new Item(80));

        graph.AddEdge(v1, v2);
        graph.AddEdge(v1, v3);
        graph.AddEdge(v2, v4);
        graph.AddEdge(v3, v4);
        graph.AddEdge(v4, v5);
        graph.AddEdge(v5, v6);
        graph.AddEdge(v5, v7);
        graph.AddEdge(v5, v8);
        graph.AddEdge(v6, v7);
        graph.AddEdge(v7, v8);

        return graph;
    }
}
