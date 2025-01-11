using CalculationEngine.Core.Tests.Stubs;
using Microsoft.Extensions.Logging;

namespace CalculationEngine.Core.Tests.Handlers;

[TestFixture(Category = "Integration", Explicit = true)]
public class DelayRequestTests : HangfireClientTestBase
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
        var logger = GetLogger<DelayRequestTests>();
        logger.LogWarning("Completed!");
        Assert.Pass();
    }
}

