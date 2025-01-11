using CalculationEngine.Core.Model;
using CalculationEngine.Core.Tests.Stubs;

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
