using CalculationEngine.Core.Model;
using CalculationEngine.Core.Extensions;
using CalculationEngine.DataAccess.Tests.Stubs;

namespace CalculationEngine.DataAccess.Tests;

internal record SimpleRecord(Guid Id, string Name, DateTime Date, decimal Amount);

internal static class GraphStubs
{
    public static CalculationGraph CreateSimpleGraph()
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

        return graph;
    }
}
