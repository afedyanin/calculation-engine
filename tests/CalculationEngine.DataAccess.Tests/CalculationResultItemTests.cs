using CalculationEngine.AppDemo.Stubs;
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

        var saved = context.CalculationResultItems.Where(x => x.Id == result.Id).SingleOrDefault();

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

        unit.Results.Add(result);

        using var context = ContextFactory.CreateDbContext();

        context.CalculationResultItems.Add(result);
        context.CalculationUnits.Add(unit);
        context.SaveChanges();

        var restored = context
            .CalculationUnits
            .Include(cu => cu.Results)
            .SingleOrDefault(cu => cu.Id == unit.Id);

        Assert.That(restored, Is.Not.Null);
        Assert.That(restored.Results, Is.Not.Empty);
        Assert.That(restored.Results.FirstOrDefault(r => r.Id == result.Id), Is.Not.Null);
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

        unit.Results.Add(result);

        using var context = ContextFactory.CreateDbContext();
        context.CalculationResultItems.Add(result);
        context.CalculationUnits.Add(unit);
        context.SaveChanges();

        var restored = context
            .CalculationUnits
            .Include(cu => cu.Results)
            .SingleOrDefault(cu => cu.Id == unit.Id);

        Assert.That(restored, Is.Not.Null);

        var item = restored.Results.FirstOrDefault(r => r.Id == result.Id);
        Assert.That(item, Is.Not.Null);

        var updatedRecord = new SimpleRecord(Guid.NewGuid(), "Updated Record", DateTime.UtcNow, 98765.43M);

        item.Content = updatedRecord;
        item.Name = "Updated";

        context.SaveChanges();

        var saved_item = context.CalculationResultItems.SingleOrDefault(i => i.Id == item.Id);
        Assert.That(saved_item, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(saved_item.Name, Is.EqualTo(item.Name));
            Assert.That(saved_item.Content, Is.EqualTo(item.Content));
        });

        var unit_restored = context
            .CalculationUnits
            .Include(cu => cu.Results)
            .SingleOrDefault(cu => cu.Id == unit.Id);

        Assert.That(unit_restored, Is.Not.Null);

        var item_restored = unit_restored.Results.FirstOrDefault(r => r.Id == item.Id);
        Assert.That(item_restored, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(item_restored.Name, Is.EqualTo(item.Name));
            Assert.That(item_restored.Content, Is.EqualTo(item.Content));
        });
    }
}
