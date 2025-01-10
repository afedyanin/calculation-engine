using CalculationEngine.AppDemo.Stubs;
using CalculationEngine.Core.Model;

namespace CalculationEngine.Core.Tests.Model;

[TestFixture(Category = "Unit")]
public class CalculationUnitTests
{
    [Test]
    public void RequestHasValidCalculationUnitId()
    {
        var request = new DelayRequest()
        {
            Delay = TimeSpan.Zero,
        };

        var unit = new CalculationUnit()
        {
            Id = Guid.NewGuid(),
            GraphId = Guid.NewGuid(),
            JobId = "None",
            Request = request,
        };

        Assert.That(request.CalculationUnitId, Is.EqualTo(unit.Id));
    }
}
