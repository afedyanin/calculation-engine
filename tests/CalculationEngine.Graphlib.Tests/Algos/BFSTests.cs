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

    [TestCase(0, "0,2,3,4,8,9")]
    [TestCase(1, "1,2,3,4,8,9")]
    [TestCase(6, "6,7,2,3,4,8,9")]
    [TestCase(5, "5,7,9,2,3,4,8")]
    public void CanPerformBFSForComplexGraph(int startIndex, string expected)
    {
        var graph = GraphFactory.CreateComplexGraph();
        var vertices = graph.BFS(startIndex);
        var actual = string.Join(',', vertices.Select(v => v.Index).ToArray());
        Assert.That(actual, Is.EqualTo(expected));
    }
}
