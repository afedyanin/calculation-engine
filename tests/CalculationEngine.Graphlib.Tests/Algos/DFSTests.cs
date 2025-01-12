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

    [TestCase(0, "0,2,3,8,9,4")]
    [TestCase(1, "1,2,3,8,9,4")]
    [TestCase(6, "6,7,2,3,8,9,4")]
    [TestCase(5, "5,7,2,3,8,9,4")]
    public void CanPerformDFSForComplexGraph(int startIndex, string expected)
    {
        var graph = GraphFactory.CreateComplexGraph();
        var vertices = graph.DFS(startIndex);
        var actual = string.Join(',', vertices.Select(v => v.Index).ToArray());
        Assert.That(actual, Is.EqualTo(expected));
    }
}
