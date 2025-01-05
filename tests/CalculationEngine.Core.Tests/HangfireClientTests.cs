using Hangfire;
using Hangfire.Common;

namespace CalculationEngine.Core.Tests;

[TestFixture(Category = "Integration", Explicit = true)]
public class HangfireClientTests : HangfireClientTestBase
{

    [Test]
    public void CanEnqueueBackgroundJob()
    {
        BackgroundJobClient.Enqueue(() => Console.WriteLine("Hello, Hangfire!"));
        Assert.Pass();
    }

    [Test]
    public void CanEnqueueRecurringJob()
    {
        RecurringJobClient.AddOrUpdate(
            "test-recurring-job-001",
            Job.FromExpression(() => Console.WriteLine("Hello, recurring!")),
            Cron.Minutely());
    }

    [Test]
    public void CanDeleteRecurringJob()
    {
        RecurringJobClient.RemoveIfExists("test-recurring-job-001");
    }

    [Test]
    public void CanGetSucceededJobs()
    {
        var jobs = MonitoringApi.SucceededJobs(1, 200);

        foreach (var job in jobs)
        {
            Console.WriteLine($"key={job.Key} succeeded={job.Value.InSucceededState}");
        }
    }

}
