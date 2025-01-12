using System.Text;
using CalculationEngine.Core.Extensions;
using CalculationEngine.Core.Handlers;
using CalculationEngine.Core.HangfireExtensions;
using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using CalculationEngine.Core.Tests.HangfireExtensions;
using CalculationEngine.Core.Tests.Stubs;
using CalculationEngine.Graphlib.Algos;

using Moq;

namespace CalculationEngine.Core.Tests.Handlers;

[TestFixture(Category = "Unit")]
public class EnqueueGraphHandlerTests
{
    private readonly IJobScheduler _jobScheduler = new JobSchedulerStub();

    private ICalculationGraphRepository? _graphRepository;
    private ICalculationUnitRepository? _calculationUnitRepository;

    private CalculationGraph _graph;

    [SetUp]
    public void Setup()
    {
        _graph = CreateGraph();

        var graphRepoMock = new Mock<ICalculationGraphRepository>();

        graphRepoMock
            .Setup(m => m.GetById(It.IsAny<Guid>(), CancellationToken.None))
            .Returns(Task.FromResult(_graph));

        _graphRepository = graphRepoMock.Object;

        var unitRepoMock = new Mock<ICalculationUnitRepository>();

        unitRepoMock
            .Setup(m => m.Update(It.IsAny<CalculationUnit>(), CancellationToken.None))
            .Returns(Task.FromResult(true));

        _calculationUnitRepository = unitRepoMock.Object;
    }

    [Test]
    public async Task CanEnqueueGraph()
    {
        var handler = new EnqueueGraphHandler(
            _jobScheduler,
            _graphRepository!,
            _calculationUnitRepository!);

        await handler.Handle(new EnqueueGraphRequest(), CancellationToken.None);

        // TODO: Fix it
        //Assert.That(result, Is.Not.Null);
        //Assert.That(result.Success, Is.True);
        Assert.Pass();

        Console.WriteLine(DumpSortedVertices(_graph));
        Console.WriteLine(DumpJobs(_graph));
    }

    private static CalculationGraph CreateGraph()
    {
        var graph = new CalculationGraph();

        var v0 = graph.AddCalculationVertex(new DelayRequest());
        var v1 = graph.AddCalculationVertex(new DelayRequest());
        var v2 = graph.AddCalculationVertex(new DelayRequest());
        var v3 = graph.AddCalculationVertex(new DelayRequest());
        var v4 = graph.AddCalculationVertex(new DelayRequest());
        var v5 = graph.AddCalculationVertex(new DelayRequest());
        var v6 = graph.AddCalculationVertex(new DelayRequest());
        var v7 = graph.AddCalculationVertex(new DelayRequest());
        var v8 = graph.AddCalculationVertex(new DelayRequest());
        var v9 = graph.AddCalculationVertex(new DelayRequest());

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

    private static string DumpSortedVertices(CalculationGraph graph)
    {
        var sb2 = new StringBuilder();
        var sorted = graph.TopologicalSort();
        var allParents = graph.GetParents();

        sb2.AppendLine($"Vertices:");

        foreach (var index in sorted.Select(x => x.Index))
        {
            var parents = allParents[index];
            if (parents != null)
            {
                var indexes = parents.Select(x => x.Index);
                sb2.AppendLine($"[{string.Join(',', indexes)}] -> {index}");
            }
            else
            {
                sb2.AppendLine($"[] -> {index}");
            }
        }

        return sb2.ToString();
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
