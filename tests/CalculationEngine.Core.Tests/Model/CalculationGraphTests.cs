using CalculationEngine.AppDemo.Stubs;
using CalculationEngine.Core.Model;
using CalculationEngine.Core.Extensions;

namespace CalculationEngine.Core.Tests.Model;

[TestFixture(Category = "Unit")]
public class CalculationGraphTests
{
    [Test]
    public void CanCreateGraph()
    {
        var graph = new CalculationGraph
        {
            Id = Guid.NewGuid(),
        };

        var v1 = graph.AddCalculationVertex(new DelayRequest());
        var v2 = graph.AddCalculationVertex(new DelayRequest());
        var v3 = graph.AddCalculationVertex(new DelayRequest());

        graph.AddEdge(v1, v2);
        graph.AddEdge(v2, v3);

        Console.WriteLine(graph.RenderVertices());
        Console.WriteLine(graph.RenderEdges());

        Assert.Pass();
    }

    [Test]
    public void CanValidateGraphWithNoCycles()
    {
        var graph = new CalculationGraph
        {
            Id = Guid.NewGuid(),
        };

        var v1 = graph.AddCalculationVertex(new DelayRequest());
        var v2 = graph.AddCalculationVertex(new DelayRequest());
        var v3 = graph.AddCalculationVertex(new DelayRequest());

        graph.AddEdge(v1, v2);
        graph.AddEdge(v2, v3);

        graph.Validate();

        Assert.Pass();
    }

    [Test]
    public void CanValidateGraphWithCycle()
    {
        var graph = new CalculationGraph
        {
            Id = Guid.NewGuid(),
        };

        var v1 = graph.AddCalculationVertex(new DelayRequest());
        var v2 = graph.AddCalculationVertex(new DelayRequest());
        var v3 = graph.AddCalculationVertex(new DelayRequest());

        graph.AddEdge(v1, v2);
        graph.AddEdge(v2, v3);
        graph.AddEdge(v3, v1);

        Assert.Throws<InvalidOperationException>(() => graph.Validate());
    }
}
