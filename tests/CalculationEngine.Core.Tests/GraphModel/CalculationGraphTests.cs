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
        _graph = new CalculationGraphOld();
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

    [Test]
    public void CanEnqueueSimple()
    {
        var request = new DelayRequest();
        var rootNode = _graph.AddRoot(request);
        var node11 = rootNode.AddChild(request);
        var node12 = node11.AddChild(request);
        var node21 = rootNode.AddChild(request);
        var node22 = node21.AddChild(request);
        var node31 = rootNode.AddChild(request);
        var node32 = node31.AddChild(request);
        var node33 = node32.AddChild(request);

        _graph.Enqueue(_scheduler);

        Assert.Multiple(() =>
        {
            Assert.That(rootNode.JobId, Is.EqualTo("1"));
            Assert.That(node11.JobId, Is.EqualTo("2"));
            Assert.That(node12.JobId, Is.EqualTo("3"));
            Assert.That(node21.JobId, Is.EqualTo("4"));
            Assert.That(node22.JobId, Is.EqualTo("5"));
        });

        Assert.Multiple(() =>
        {
            Assert.That(rootNode.Level, Is.EqualTo(0));
            Assert.That(node11.Level, Is.EqualTo(1));
            Assert.That(node12.Level, Is.EqualTo(2));
            Assert.That(node21.Level, Is.EqualTo(1));
            Assert.That(node22.Level, Is.EqualTo(2));
            Assert.That(node31.Level, Is.EqualTo(1));
            Assert.That(node32.Level, Is.EqualTo(2));
            Assert.That(node33.Level, Is.EqualTo(3));
        });

        Console.WriteLine(_graph.Render());
    }

    [Test]
    public void CanEnqueueWithJoin()
    {
        var request = new DelayRequest();
        var rootNode = _graph.AddRoot(request);
        var node11 = rootNode.AddChild(request);
        var node12 = node11.AddChild(request);
        var node21 = rootNode.AddChild(request);
        var node22 = node21.AddChild(request);
        var node31 = rootNode.AddChild(request);
        var node32 = node31.AddChild(request);
        var node33 = node32.AddChild(request);

        var join01 = _graph.Join([node22, node32], request);

        _graph.Enqueue(_scheduler);

        Assert.Multiple(() =>
        {
            Assert.That(join01.Parents.Count, Is.EqualTo(2));
        });

        Console.WriteLine(_graph.Render());
    }
}
