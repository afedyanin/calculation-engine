using CalculationEngine.Core.Extensions;
using CalculationEngine.Core.GraphModel;
using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using CalculationEngine.Graphlib;
using CalculationEngine.Graphlib.Algos;
using MediatR;

namespace CalculationEngine.Core.Handlers;

internal class EnqueueGraphHandler : IRequestHandler<EnqueueGraphRequest, EnqueueGraphResponse>
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

    public async Task<EnqueueGraphResponse> Handle(EnqueueGraphRequest request, CancellationToken cancellationToken)
    {
        var graph = await _calculationGraphRepository.GetById(request.GraphId);

        var message = Validate(graph);

        if (message != null)
        {
            return message;
        }

        var jobs = new HashSet<string>();
        var vertices = graph!.TopologicalSort();

        foreach (var vertex in vertices)
        {
            var jobId = await EnqueueVertex(vertex, null, cancellationToken);
            jobs.Add(jobId);

            foreach (var child in vertex.Children)
            {
                var childJobId = await EnqueueVertex(child, jobId, cancellationToken);
                jobs.Add(childJobId);
            }
        }

        var jobIds = string.Join(',', jobs);
        var response = new EnqueueGraphResponse()
        {
            Success = true,
            Message = $"Graph enqueued. Jobs=[{jobIds}]",
        };

        return response;
    }

    private async Task<string> EnqueueVertex(
        Vertex<CalculationUnit> vertex,
        string? parentJobId = null,
        CancellationToken cancellationToken = default)
    {
        var calculationUnit = vertex.Value;

        if (!string.IsNullOrEmpty(calculationUnit.JobId))
        {
            return calculationUnit.JobId;
        }

        calculationUnit.JobId = EnqueueCalculationUnit(calculationUnit, parentJobId);
        await _calculationUnitRepository.Update(calculationUnit, cancellationToken);

        return calculationUnit.JobId;
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

        if (graph.HasInvalidInDegreeVertices())
        {
            // TODO: fix graph vertex for inDegree
            return new EnqueueGraphResponse
            {
                Success = false,
                Message = $"Graph has invalid indegrees vertices. GraphId={graph.Id}"
            };
        }

        return null;
    }

    private string EnqueueCalculationUnit(CalculationUnit calculationUnit, string? parentJobId = null)
        => string.IsNullOrEmpty(parentJobId) ?
            _jobScheduler.Enqueue(calculationUnit.Request) :
            _jobScheduler.EnqueueAfter(parentJobId, calculationUnit.Request);
}
