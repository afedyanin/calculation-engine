using System.Text;
using CalculationEngine.Core.HangfireExtensions;
using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using CalculationEngine.Graphlib;
using CalculationEngine.Graphlib.Algos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculationEngine.Core.Handlers;

internal class EnqueueGraphHandler : IRequestHandler<EnqueueGraphRequest>
{
    private readonly IJobScheduler _jobScheduler;
    private readonly ICalculationGraphRepository _calculationGraphRepository;
    private readonly ICalculationUnitRepository _calculationUnitRepository;
    private readonly ILogger<EnqueueGraphHandler> _logger;

    public EnqueueGraphHandler(
        IJobScheduler jobScheduler,
        ICalculationGraphRepository calculationGraphRepository,
        ICalculationUnitRepository calculationUnitRepository,
        ILogger<EnqueueGraphHandler> logger)
    {
        _jobScheduler = jobScheduler;
        _calculationGraphRepository = calculationGraphRepository;
        _calculationUnitRepository = calculationUnitRepository;
        _logger = logger;
    }

    public async Task Handle(EnqueueGraphRequest request, CancellationToken cancellationToken)
    {
        var graph = await _calculationGraphRepository.GetById(request.GraphId);

        var errorMessage = Validate(graph);

        if (errorMessage != null)
        {
            _logger.LogError($"Error: {errorMessage}");
            return;
        }

        var jobs = new HashSet<string>();
        var sortedVertices = graph!.TopologicalSort();
        var allParents = graph!.GetParents();

        foreach (var vertex in sortedVertices)
        {
            var jobId = await EnqueueVertex(vertex, allParents[vertex.Index]);
            vertex.Value.JobId = jobId;
            await _calculationUnitRepository.Update(vertex.Value, cancellationToken);

            jobs.Add(jobId);
        }

        var jobIds = string.Join(',', jobs);

        _logger.LogInformation($"GraphId={request.GraphId} enqueued. Jobs=[{jobIds}]");

        // Dump job schedule
        var updatedGraph = await _calculationGraphRepository.GetById(graph!.Id);
        var jobSchedule = DumpJobs(updatedGraph!);
        _logger.LogInformation($"Job schedule for GraphId={request.GraphId}: {jobSchedule}");
    }

    private async Task<string> EnqueueVertex(
        Vertex<CalculationUnit> vertex,
        List<Vertex<CalculationUnit>>? parents)
    {
        if (!string.IsNullOrEmpty(vertex.Value.JobId))
        {
            // already enqueued
            return vertex.Value.JobId;
        }

        // Исток
        if (parents == null || parents.Count == 0)
        {
            return _jobScheduler.Enqueue(vertex.Value.Request);
        }

        // Один источник
        if (parents.Count == 1)
        {
            var parentJobId = parents[0].Value.JobId!;
            return _jobScheduler.EnqueueAfter(parentJobId, vertex.Value.Request);
        }

        // Много источников. Нужна синхронизация
        var parentJobIds = parents.Select(p => p.Value.JobId!).ToArray();
        var awatingRequest = new JobAwaitingRequest
        {
            CalculationUnitId = vertex.Value.Id,
            JobIds = parentJobIds,
        };

        var awatingJobId = _jobScheduler.Enqueue(awatingRequest);

        _logger.LogInformation($"Enqueued awatingJobId={awatingJobId} parentJobIds=[{string.Join(',', parentJobIds)}]");

        return _jobScheduler.EnqueueAfter(awatingJobId, vertex.Value.Request);
    }

    private string? Validate(CalculationGraph? graph)
    {
        if (graph == null)
        {
            return $"Cannot find graph with specified GraphId.";
        }

        if (graph.HasAnyCycle())
        {
            return $"Cannot enqueue graph with cycles. GraphId={graph.Id}";
        }

        return null;
    }

    private static string DumpJobs(CalculationGraph graph)
    {
        var sb2 = new StringBuilder();
        var sorted = graph.TopologicalSort();
        var allParents = graph.GetParents();

        sb2.AppendLine($"Jobs:");

        foreach (var index in sorted.Select(x => x.Index))
        {
            var parents = allParents[index];

            var jobId = graph.Vertices[index].Value.JobId;
            if (parents != null)
            {
                var jobs = parents.Select(x => x.Value.JobId);
                sb2.AppendLine($"[{string.Join(',', jobs)}] -> {jobId}");
            }
            else
            {
                sb2.AppendLine($"[] -> {jobId}");
            }
        }

        return sb2.ToString();
    }

}
