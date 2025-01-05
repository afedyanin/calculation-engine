using Hangfire;

namespace CalculationEngine.Core.Tests;

[TestFixture(Category = "Integration", Explicit = true)]
public class HangfireClientTests : HangfireClientTestBase
{
    [Test]
    public void CanEnqueueJob()
    {
        BackgroundJobClient.Enqueue(() => Console.WriteLine("Hello, Hangfire!"));
        Assert.Pass();
    }
}
