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
    public void CyclicGraphWithRemovedOneCycleHasCycles()
    {
        var graph = GraphFactory.CreateCyclicGraph();
        var v1 = graph.Vertices[]
        var hasCycle = graph.HasAnyCycle();
        Assert.That(hasCycle, Is.True);
    }
}
