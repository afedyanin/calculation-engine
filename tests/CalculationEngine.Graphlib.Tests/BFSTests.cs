namespace CalculationEngine.Graphlib.Tests;

[TestFixture(Category = "Unit")]
public class BFSTests
{
    [Test]
    public void CanTravesreBFS()
    {
        var graph = GraphFactory.CreateSimpleGraph();
        var vertices = graph.DFS();
        vertices.ForEach(Console.WriteLine);

        Assert.Pass();
    }
}
