using CalculationEngine.Graphlib.Algos;

namespace CalculationEngine.Graphlib.Tests.Algos;

[TestFixture(Category = "Unit")]
public class DFSTests
{
    [Test]
    public void CanPerformDFSForSimpleGraph()
    {
        var graph = GraphFactory.CreateSimpleGraph();
        var vertices = graph.DFS();
        //vertices.ForEach(Console.WriteLine);

        var actual = vertices.Select(v => v.Index + 1).ToArray();
        var expected = new int[] { 1, 2, 4, 5, 6, 7, 8, 3 };

        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [Test]
    public void CanPerformDFSForCyclicGraph()
    {
        var graph = GraphFactory.CreateCyclicGraph();
        var vertices = graph.DFS();
        //vertices.ForEach(Console.WriteLine);

        var actual = vertices.Select(v => v.Index + 1).ToArray();
        var expected = new int[] { 1, 2, 4, 5, 6, 7, 8, 3 };

        Assert.That(actual, Is.EquivalentTo(expected));
    }

}
