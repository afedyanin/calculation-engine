using System.Text;
using CalculationEngine.Core.Extensions;
using CalculationEngine.Core.Handlers;
using CalculationEngine.Core.HangfireExtensions;
using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using CalculationEngine.Graphlib.Algos;
using MediatR;
using Sample.Application.CalculationUnits;

namespace Sample.Application.ReportApi;
internal class EnqueueReportHandler : IRequestHandler<EnqueueReportRequest, EnqueueReportResponse>
{
    private const int maxDelayInSeconds = 20;

    private readonly ICalculationGraphRepository _calculationGraphRepository;
    private readonly IJobScheduler _jobScheduler;

    public EnqueueReportHandler(
        ICalculationGraphRepository calculationGraphRepository,
        IJobScheduler jobScheduler)
    {
        _calculationGraphRepository = calculationGraphRepository;
        _jobScheduler = jobScheduler;
    }

    public async Task<EnqueueReportResponse> Handle(EnqueueReportRequest request, CancellationToken cancellationToken)
    {
        var graph = CreateCalculationGraph(out Guid resultUnitId);
        var saved = await _calculationGraphRepository.Insert(graph);

        if (!saved)
        {
            return new EnqueueReportResponse
            {
                Message = "Cannot save calculation graph",
            };
        }

        // Schedule to execute graph
        var jobId = _jobScheduler.Enqueue(
            new EnqueueGraphRequest
            {
                GraphId = graph.Id,
            });

        var response = new EnqueueReportResponse
        {
            GraphId = graph.Id,
            JobId = jobId,
            ReportResultCalculationUnitId = resultUnitId,
            GraphVerticesTraversal = DumpVertices(graph),
        };

        return response;
    }

    private CalculationGraph CreateCalculationGraph(out Guid reportResultUnitId)
    {
        var graph = new CalculationGraph();

        var v0 = graph.AddCalculationVertex(CreateRequest("Blue"));
        var v1 = graph.AddCalculationVertex(CreateRequest("Red"));
        var v2 = graph.AddCalculationVertex(CreateRequest("Green"));
        var v3 = graph.AddCalculationVertex(CreateRequest("Blue"));
        var v4 = graph.AddCalculationVertex(CreateRequest("Red"));
        var v5 = graph.AddCalculationVertex(CreateRequest("Blue"));
        var v6 = graph.AddCalculationVertex(CreateRequest("Green"));
        var v7 = graph.AddCalculationVertex(CreateRequest("Blue"));
        var v8 = graph.AddCalculationVertex(CreateRequest("Red"));

        var v9 = graph.AddCalculationVertex(new HarvestResultsRequest());
        reportResultUnitId = v9.Value.Id;

        graph.AddEdge(v0, v2);
        graph.AddEdge(v1, v2);
        graph.AddEdge(v7, v2);
        graph.AddEdge(v6, v7);
        graph.AddEdge(v5, v7);
        graph.AddEdge(v2, v3);
        graph.AddEdge(v2, v4);
        graph.AddEdge(v3, v8);
        graph.AddEdge(v4, v8);
        graph.AddEdge(v8, v9);
        graph.AddEdge(v5, v9);

        return graph;
    }

    private ColoredCalcRequest CreateRequest(string color)
        => new ColoredCalcRequest
        {
            Color = color,
            Delay = TimeSpan.FromSeconds(Random.Shared.Next(maxDelayInSeconds))
        };

    private static string DumpVertices(CalculationGraph graph)
    {
        var sb = new StringBuilder();
        var sorted = graph.TopologicalSort();
        var allParents = graph.GetParents();

        sb.AppendLine($"Vertices:");

        foreach (var index in sorted.Select(x => x.Index))
        {
            var parents = allParents[index];
            if (parents != null)
            {
                var indexes = parents.Select(x => x.Index);
                sb.AppendLine($"[{string.Join(',', indexes)}] -> {index}");
            }
            else
            {
                sb.AppendLine($"[] -> {index}");
            }
        }

        return sb.ToString();
    }
}
