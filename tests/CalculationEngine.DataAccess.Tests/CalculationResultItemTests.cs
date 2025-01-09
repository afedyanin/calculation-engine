using CalculationEngine.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess.Tests;

[TestFixture(Category = "Database", Explicit = true)]
public class CalculationResultItemTests : DbTestBase
{
    private record SimpleRecord(Guid Id, string Name, DateTime Date, decimal Amount);

    [Test]
    public void CanGetCalculationResultItems()
    {
        var items = Context.CalculationResultItems.AsNoTracking().ToArray();
        Assert.Pass();
    }

    [Test]
    public void CanCreateCalculationResultItem()
    {
        var record = new SimpleRecord(Guid.NewGuid(), "Just Record", DateTime.UtcNow, 1234.56M);
        var result = new CalculationResultItem()
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = Guid.NewGuid(),
            Name = "Name",
            Content = record,
        };

        Context.CalculationResultItems.Add(result);
        Context.SaveChanges();

        var saved = Context.CalculationResultItems.Where(x => x.Id == result.Id).SingleOrDefault();

        Assert.That(saved, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(saved.CalculationUnitId, Is.EqualTo(result.CalculationUnitId));
            Assert.That(saved.Name, Is.EqualTo(result.Name));
            Assert.That(saved.ContentJson, Is.EqualTo(result.ContentJson));
            Assert.That(saved.ContentType, Is.EqualTo(result.ContentType));
        });

        var restored = saved.Content as SimpleRecord;

        Assert.That(restored, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(restored.Id, Is.EqualTo(record.Id));
            Assert.That(restored.Name, Is.EqualTo(record.Name));
            Assert.That(restored.Date, Is.EqualTo(record.Date));
            Assert.That(restored.Amount, Is.EqualTo(record.Amount));
        });
    }
}
