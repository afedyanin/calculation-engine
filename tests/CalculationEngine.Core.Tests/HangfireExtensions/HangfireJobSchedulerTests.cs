using CalculationEngine.AppDemo.Stubs;
using CalculationEngine.Core.GraphModel;
using CalculationEngine.Core.Handlers;

namespace CalculationEngine.Core.Tests.HangfireExtensions;

[TestFixture(Category = "Integration", Explicit = true)]
public class HangfireJobSchedulerTests : HangfireClientTestBase
{
    [Test]
    public void CanEnqueueSimpleRequest()
    {
        var request = new DelayRequest();

        var jobId = JobScheduler.Enqueue(request);
        Assert.That(jobId, Is.Not.Null);

        Console.WriteLine($"jobId={jobId}");
    }

    [Test]
    public void CanEnqueueGraph()
    {
        var graph = new CalculationGraphOld();
        var request = new DelayRequest();

        var rootNode = graph.AddRoot(request);
        var node11 = rootNode.AddChild(request);
        var node12 = node11.AddChild(request);
        var node21 = rootNode.AddChild(request);
        var node22 = node21.AddChild(request);
        var node31 = rootNode.AddChild(request);
        var node32 = node31.AddChild(request);
        var node33 = node32.AddChild(request);
        var join01 = graph.Join([node22, node32], request);

        graph.Enqueue(JobScheduler);

        Console.WriteLine(graph.Render());
    }
    [Test]
    public void CanEnqueueSimpleJob()
    {
        var request = new DelayRequest
        {
            CorrelationId = Guid.NewGuid(),
            Delay = TimeSpan.FromSeconds(1),
        };

        var jobId = JobScheduler.Enqueue(request);

        Assert.That(jobId, Is.Not.Empty);

        Console.WriteLine($"Enqueued job={jobId}");
    }

    [Test]
    public void CanEnqueueSequenceOfJobs()
    {
        var request = new DelayRequest
        {
            CorrelationId = Guid.NewGuid(),
            Delay = TimeSpan.FromSeconds(1),
        };

        var jobId1 = JobScheduler.Enqueue(request);
        var jobId2 = JobScheduler.EnqueueAfter(jobId1, request);
        var jobId3 = JobScheduler.EnqueueAfter(jobId2, request);
        var jobId4 = JobScheduler.EnqueueAfter(jobId3, request);

        Assert.Multiple(() =>
        {
            Assert.That(jobId1, Is.Not.Empty);
            Assert.That(jobId2, Is.Not.Empty);
            Assert.That(jobId3, Is.Not.Empty);
            Assert.That(jobId4, Is.Not.Empty);
        });

        var jobIds = new string[] { jobId1, jobId2, jobId3, jobId4 };
        Console.WriteLine($"Enqueued jobs={string.Join(',', jobIds)}");
    }

    [Test]
    public void CanAwaitSequenceOfJobs()
    {
        var request = new DelayRequest
        {
            CorrelationId = Guid.NewGuid(),
            Delay = TimeSpan.FromSeconds(1),
        };

        var jobId11 = JobScheduler.Enqueue(request);
        var jobId12 = JobScheduler.EnqueueAfter(jobId11, request);
        var jobId21 = JobScheduler.Enqueue(request);
        var jobId22 = JobScheduler.EnqueueAfter(jobId21, request);

        var jobIds = new string[] { jobId11, jobId12, jobId21, jobId22 };

        var awaitRequest = new JobAwaitingRequest
        {
            JobIds = jobIds,
            CorrelationId = request.CorrelationId,
            PoolingInterval = TimeSpan.FromSeconds(1),
        };

        var awatingJobId = JobScheduler.Enqueue(awaitRequest);

        var jobId31 = JobScheduler.EnqueueAfter(awatingJobId, request);

        Console.WriteLine($"Enqueued jobs={string.Join(',', jobIds)}");
        Console.WriteLine($"Awating awatingJobId={awatingJobId} ConinuationJobId={jobId31}");
        Assert.Pass();
    }
}
