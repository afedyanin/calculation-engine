using Hangfire;
using Hangfire.Common;
using Hangfire.Storage;

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

    [Test]
    public async Task CanGetJobHistory()
    {
        //var jobId = BackgroundJobClient.Enqueue(() => Console.WriteLine("Hello, Hangfire!"));
        // await Task.Delay(4000);

        var jobId = "102";

        var jobDeatils = MonitoringApi.JobDetails(jobId);

        Assert.That(jobDeatils, Is.Not.Null);

        foreach (var item in jobDeatils.History)
        {
            Console.WriteLine($"JobId={jobId} CreatedAt={item.CreatedAt} StateName={item.StateName} Reason={item.Reason}");
        }
    }

    [Test]
    public void CanGetLastState()
    {
        var jobId = "102";

        var jobDeatils = MonitoringApi.JobDetails(jobId);
        Assert.That(jobDeatils, Is.Not.Null);

        var lastState = jobDeatils.History.OrderBy(x => x.CreatedAt).Last();
        Assert.That(lastState, Is.Not.Null);

        Console.WriteLine($"JobId={jobId} CreatedAt={lastState.CreatedAt} StateName={lastState.StateName} Reason={lastState.Reason}");
    }
}
