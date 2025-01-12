using CalculationEngine.Core.Model;
using Sample.Application.ReportModel;

namespace CalculationEngine.DataAccess.Tests.Repositories;

[TestFixture(Category = "Database", Explicit = true)]

public class CalculationResultsRepositoryTests : DbTestBase
{
    [Test]
    public async Task CanSaveNewResultItem()
    {
        var graph = GraphStubs.CreateSimpleGraph();
        var unitId = graph.Vertices[1].Value.Id;

        await CalculationGraphRepository.Insert(graph);

        var record = new SimpleRecord(Guid.NewGuid(), "Just Record", DateTime.UtcNow, 1234.56M);
        var result = new CalculationResultItem()
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = unitId,
            Name = "Name",
            Content = record,
        };

        var isSaved = await CalculationResultRepository.Insert(result);
        Assert.That(isSaved, Is.True);

        Console.WriteLine($"UnitId={unitId} ResultId={result.Id}");
    }

    [Test]
    public async Task CanUpdateResultItem()
    {
        var graph = GraphStubs.CreateSimpleGraph();
        var unitId = graph.Vertices[1].Value.Id;

        await CalculationGraphRepository.Insert(graph);

        var record = new SimpleRecord(Guid.NewGuid(), "Just Record", DateTime.UtcNow, 1234.56M);
        var result = new CalculationResultItem()
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = unitId,
            Name = "Name",
            Content = record,
        };

        var isSaved = await CalculationResultRepository.Insert(result);
        Assert.That(isSaved, Is.True);

        var restored = await CalculationResultRepository.GetById(result.Id);
        Assert.That(restored, Is.Not.Null);

        restored.Name = "UPDATED";
        isSaved = await CalculationResultRepository.Update(restored);
        Assert.That(isSaved, Is.True);

        var loaded = await CalculationResultRepository.GetById(result.Id);
        Assert.That(loaded, Is.Not.Null);
        Assert.That(loaded.Name, Is.EqualTo(restored.Name));
    }

    [TestCase("800e79b2-d6cc-4f3e-9603-b5ef4c69e1ec")]
    public async Task CanLoadReportResult(string resultItemId)
    {
        var id = new Guid(resultItemId);
        var item = await CalculationResultRepository.GetById(id);
        Assert.That(item, Is.Not.Null);

        var typed = item.Content as ReportDataItem;
        Assert.That(typed, Is.Not.Null);

        Console.WriteLine($"Color={typed.Color}");
    }
}
