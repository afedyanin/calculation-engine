using CalculationEngine.AppDemo.Stubs;
using CalculationEngine.Core.GraphModel;
using CalculationEngine.Core.HangfireExtensions;

namespace CalculationEngine.Core.Tests.HangfireExtensions;

[TestFixture(Category = "Integration", Explicit = true)]
public class HangfireJobSchedulerTests : HangfireClientTestBase
{
    [Test]
    public void CanEnqueueSimpleRequest()
    {
        var scheduler = new HangfireJobScheduler(BackgroundJobClient);
        var request = new DelayRequest();

        var jobId = scheduler.Enqueue(request);
        Assert.That(jobId, Is.Not.Null);

        Console.WriteLine($"jobId={jobId}");
    }

    [Test]
    public void CanEnqueueGraph()
    {
        var graph = new CalculationGraph();
        var scheduler = new HangfireJobScheduler(BackgroundJobClient);

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

        graph.Enqueue(scheduler);

        Console.WriteLine(graph.Render());
    }
}
