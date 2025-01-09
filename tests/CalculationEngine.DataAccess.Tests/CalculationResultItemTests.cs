using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess.Tests;

[TestFixture(Category = "Database", Explicit = true)]
public class CalculationResultItemTests : DbTestBase
{
    [Test]
    public void CanGetCalculationResultItems()
    {
        var items = Context.CalculationResultItems.AsNoTracking().ToArray();
        Assert.Pass();
    }
}
