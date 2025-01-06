using CalculationEngine.AppDemo.Stubs;
using CalculationEngine.Core.GraphModel;

namespace CalculationEngine.Core.Tests.GraphModel;

[TestFixture(Category = "Unit")]
public class CalculationGraphTests
{
    private ICalculationGraph _graph;
    private IJobScheduler _scheduler;

    [SetUp]
    public void SetUp()
    {
        _graph = new CalculationGraph();
        _scheduler = new JobSchedulerStub();
    }

    [Test]
    public void CanCreateRootNode()
    {
        var request = new DelayRequest();
        var rootNode = _graph.AddRoot(request);

        Assert.That(rootNode, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(rootNode.Level, Is.EqualTo(0));
            Assert.That(rootNode.Graph, Is.EqualTo(_graph));
            Assert.That(rootNode.Request, Is.EqualTo(request));
            Assert.That(rootNode.Parents.Count, Is.EqualTo(0));
            Assert.That(rootNode.Children.Count, Is.EqualTo(0));
            Assert.That(rootNode.JobId, Is.Null);
        });
    }

    [Test]
    public void CanEnqueueRootNode()
    {
        var request = new DelayRequest();
        var rootNode = _graph.AddRoot(request);
        _graph.Enqueue(_scheduler);

        Assert.That(rootNode.JobId, Is.EqualTo("1"));

        Console.WriteLine(_graph.Render());
    }
}
