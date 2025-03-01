using CalculationEngine.Core.Model;
using CalculationEngine.DataAccess.Tests.Stubs;
using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess.Tests;

[TestFixture(Category = "Database", Explicit = true)]
public class CalculationResultItemTests : DbTestBase
{

    [Test]
    public void CanGetCalculationResultItems()
    {
        using var context = ContextFactory.CreateDbContext();
        var items = context.CalculationResultItems.AsNoTracking().ToArray();
        Assert.Pass();
    }

    [Test]
    public void CanCreateCalculationResultItem()
    {
        var unit = new CalculationUnit()
        {
            Id = Guid.NewGuid(),
            GraphId = Guid.NewGuid(),
            JobId = "None",
            Request = new DelayRequest(),
        };


        var record = new SimpleRecord(Guid.NewGuid(), "Just Record", DateTime.UtcNow, 1234.56M);
        var result = new CalculationResultItem()
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = unit.Id,
            Name = "Name",
            Content = record,
        };

        using var context = ContextFactory.CreateDbContext();
        context.CalculationUnits.Add(unit);
        context.CalculationResultItems.Add(result);
        context.SaveChanges();

        using var context2 = ContextFactory.CreateDbContext();
        var saved = context2.CalculationResultItems.Where(x => x.Id == result.Id).SingleOrDefault();

        Assert.That(saved, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(saved.CalculationUnitId, Is.EqualTo(result.CalculationUnitId));
            Assert.That(saved.Name, Is.EqualTo(result.Name));
            //Assert.That(saved.ContentJson, Is.EqualTo(result.ContentJson));
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

    [Test]
    public void CanSaveAddedCalculationResultForUnit()
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

        var record = new SimpleRecord(Guid.NewGuid(), "Just Record", DateTime.UtcNow, 1234.56M);

        var result = new CalculationResultItem()
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = unit.Id,
            Name = "Name",
            Content = record,
        };

        using var context = ContextFactory.CreateDbContext();

        context.CalculationUnits.Add(unit);
        context.CalculationResultItems.Add(result);
        context.SaveChanges();

        using var context2 = ContextFactory.CreateDbContext();

        var restored = context2
            .CalculationResultItems
            .SingleOrDefault(r => r.CalculationUnitId == unit.Id);

        Assert.That(restored, Is.Not.Null);
    }

    [Test]
    public void CanSaveUpdatedCalculationResultForUnit()
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

        var record = new SimpleRecord(Guid.NewGuid(), "Just Record", DateTime.UtcNow, 1234.56M);

        var result = new CalculationResultItem()
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = unit.Id,
            Name = "Name",
            Content = record,
        };

        using var context = ContextFactory.CreateDbContext();

        context.CalculationUnits.Add(unit);
        context.CalculationResultItems.Add(result);

        context.SaveChanges();

        using var context2 = ContextFactory.CreateDbContext();

        var restored = context2
            .CalculationResultItems
            .SingleOrDefault(r => r.CalculationUnitId == unit.Id);

        Assert.That(restored, Is.Not.Null);

        var updatedRecord = new SimpleRecord(Guid.NewGuid(), "Updated Record", DateTime.UtcNow, 98765.43M);

        restored.Content = updatedRecord;
        restored.Name = "Updated";

        context2.SaveChanges();

        using var context3 = ContextFactory.CreateDbContext();

        var saved_item = context3.CalculationResultItems.SingleOrDefault(i => i.Id == restored.Id);
        Assert.That(saved_item, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(saved_item.Name, Is.EqualTo(restored.Name));
            Assert.That(saved_item.Content, Is.EqualTo(restored.Content));
        });

        var unit_restored = context3
            .CalculationResultItems
            .SingleOrDefault(r => r.CalculationUnitId == unit.Id);

        Assert.That(unit_restored, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(unit_restored.Name, Is.EqualTo(saved_item.Name));
            Assert.That(unit_restored.Content, Is.EqualTo(saved_item.Content));
        });
    }
}
