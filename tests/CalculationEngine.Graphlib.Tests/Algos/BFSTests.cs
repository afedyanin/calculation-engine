using CalculationEngine.Graphlib.Algos;

namespace CalculationEngine.Graphlib.Tests.Algos;

[TestFixture(Category = "Unit")]
public class BFSTests
{
    [Test]
    public void CanPerformBFSForSimpleGraph()
    {
        var graph = GraphFactory.CreateSimpleGraph();
        var vertices = graph.BFS();
        //vertices.ForEach(Console.WriteLine);

        var actual = vertices.Select(v => v.Index + 1).ToArray();
        var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [Test]
    public void CanPerformBFSForCyclicGraph()
    {
        var graph = GraphFactory.CreateCyclicGraph();
        var vertices = graph.BFS();
        //vertices.ForEach(Console.WriteLine);

        var actual = vertices.Select(v => v.Index + 1).ToArray();
        var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        Assert.That(actual, Is.EquivalentTo(expected));
    }

}
