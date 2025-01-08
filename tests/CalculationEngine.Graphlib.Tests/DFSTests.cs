namespace CalculationEngine.Graphlib.Tests;

[TestFixture(Category = "Unit")]
public class DFSTests
{
    [Test]
    public void CanTravesreDFS()
    {
        var graph = GraphFactory.CreateSimpleGraph();
        var vertices = graph.DFS();
        vertices.ForEach(Console.WriteLine);

        Assert.Pass();
    }

}
