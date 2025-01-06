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

    [Test]
    public void CanEnqueueSequenceOfJobs()
    {
        var request = new DelayRequest
        {
            CorrelationId = Guid.NewGuid(),
            Delay = TimeSpan.FromSeconds(1),
        };

        var jobId1 = BackgroundJobClient.EnqueueBackgroundJob(request);
        var jobId2 = BackgroundJobClient.ContinueWithBackgropundJob(jobId1, request);
        var jobId3 = BackgroundJobClient.ContinueWithBackgropundJob(jobId2, request);
        var jobId4 = BackgroundJobClient.ContinueWithBackgropundJob(jobId3, request);

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
}
