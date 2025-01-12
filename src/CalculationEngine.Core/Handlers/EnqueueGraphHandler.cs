using CalculationEngine.Core.HangfireExtensions;
using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using CalculationEngine.Graphlib;
using CalculationEngine.Graphlib.Algos;
using MediatR;

namespace CalculationEngine.Core.Handlers;

internal class EnqueueGraphHandler : IRequestHandler<EnqueueGraphRequest>
{
    private readonly IJobScheduler _jobScheduler;
    private readonly ICalculationGraphRepository _calculationGraphRepository;
    private readonly ICalculationUnitRepository _calculationUnitRepository;

    public EnqueueGraphHandler(
        IJobScheduler jobScheduler,
        ICalculationGraphRepository calculationGraphRepository,
        ICalculationUnitRepository calculationUnitRepository)
    {
        _jobScheduler = jobScheduler;
        _calculationGraphRepository = calculationGraphRepository;
        _calculationUnitRepository = calculationUnitRepository;
    }

    public async Task Handle(EnqueueGraphRequest request, CancellationToken cancellationToken)
    {
        var graph = await _calculationGraphRepository.GetById(request.GraphId);

        var errorResponse = Validate(graph);

        if (errorResponse != null)
        {
            // TODO: Log eror
            return;
        }

        var jobs = new HashSet<string>();
        var sortedVertices = graph!.TopologicalSort();
        var allParents = graph!.GetParents();

        foreach (var vertex in sortedVertices)
        {
            var jobId = await EnqueueVertex(vertex, allParents[vertex.Index]);
            jobs.Add(jobId);

            vertex.Value.JobId = jobId;
            await _calculationUnitRepository.Update(vertex.Value, cancellationToken);
        }

        var jobIds = string.Join(',', jobs);

        // TODO: Log Success
        var response = new EnqueueGraphResponse()
        {
            Success = true,
            Message = $"Graph enqueued. Jobs=[{jobIds}]",
        };
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
        return _jobScheduler.EnqueueAfter(awatingJobId, vertex.Value.Request);
    }

    private EnqueueGraphResponse? Validate(CalculationGraph? graph)
    {
        if (graph == null)
        {
            return new EnqueueGraphResponse
            {
                Success = false,
                Message = $"Cannot find graph with specified GraphId."
            };
        }

        if (graph.HasAnyCycle())
        {
            return new EnqueueGraphResponse
            {
                Success = false,
                Message = $"Cannot enqueue graph with cycles. GraphId={graph.Id}"
            };
        }

        return null;
    }
}
