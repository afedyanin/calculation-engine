using CalculationEngine.AppDemo.Stubs;
using CalculationEngine.Core.HangfireExtensions;

namespace CalculationEngine.Core.Tests.HangfireExtensions;

[TestFixture(Category = "Integration", Explicit = true)]
public class BacgroundJobClientExtensionsTests : HangfireClientTestBase
{
    [Test]
    public void CanEnqueueSimpleJob()
    {
        var request = new DelayRequest
        {
            CorrelationId = Guid.NewGuid(),
            Delay = TimeSpan.FromSeconds(1),
        };

        var jobId = BackgroundJobClient.EnqueueBackgroundJob(request);

        Assert.That(jobId, Is.Not.Empty);

        Console.WriteLine($"Enqueued job={jobId}");
    }
}
