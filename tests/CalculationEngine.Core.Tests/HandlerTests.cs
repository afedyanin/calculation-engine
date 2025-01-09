using CalculationEngine.AppDemo.Stubs;
using Microsoft.Extensions.Logging;

namespace CalculationEngine.Core.Tests;

[TestFixture(Category = "Integration", Explicit = true)]
public class HandlerTests : HangfireClientTestBase
{
    [Test]
    public async Task CanSendDelayRequest()
    {
        var request = new DelayRequest
        {
            CalculationUnitId = Guid.NewGuid(),
            Delay = TimeSpan.FromSeconds(1),
        };

        await Mediator.Send(request, CancellationToken.None);

        Assert.Pass();
    }

    [Test]
    public void CanLogToConsole()
    {
        var logger = GetLogger<HandlerTests>();
        logger.LogWarning("Completed!");
        Assert.Pass();
    }
}

