using CalculationEngine.Core.Model;

namespace CalculationEngine.DataAccess.Tests.Repositories;

[TestFixture(Category = "Database", Explicit = true)]
public class CalculationUnitRepositoryTests : DbTestBase
{
    [Test]
    public async Task CanUpdateCalculationUnit()
    {
        var graph = GraphStubs.CreateSimpleGraph();
        var unitId = graph.Vertices[1].Value.Id;

        await CalculationGraphRepository.Save(graph);

        var calculationUnit = await CalculationUnitRepository.GetById(unitId);
        Assert.That(calculationUnit, Is.Not.Null);

        calculationUnit.JobId = "Updated JobId";

        var isUnitSaved = await CalculationUnitRepository.Update(calculationUnit);
        Assert.That(isUnitSaved, Is.True);
    }

    [Test]
    public async Task CanSaveNewResultSet()
    {
        var graph = GraphStubs.CreateSimpleGraph();
        var unitId = graph.Vertices[1].Value.Id;

        await CalculationGraphRepository.Save(graph);

        var calculationUnit = await CalculationUnitRepository.GetById(unitId);
        Assert.That(calculationUnit, Is.Not.Null);

        var record = new SimpleRecord(Guid.NewGuid(), "Just Record", DateTime.UtcNow, 1234.56M);
        var result = new CalculationResultItem()
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = unitId,
            Name = "Name",
            Content = record,
        };

        calculationUnit.JobId = "Updated JobId with ResultSet";
        calculationUnit.Results.Add(result);

        var isUnitSaved = await CalculationUnitRepository.Update(calculationUnit);
        Assert.That(isUnitSaved, Is.True);
    }

    [Test]
    public async Task CanUpdateCalculationResultSet()
    {
        var graph = GraphStubs.CreateSimpleGraph();

        var isSaved = await CalculationGraphRepository.Save(graph);
        Assert.That(isSaved, Is.True);

        var v2 = graph.Vertices[1];
        var record = new SimpleRecord(Guid.NewGuid(), "Just Record", DateTime.UtcNow, 1234.56M);

        var result = new CalculationResultItem()
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = v2.Value.Id,
            Name = "Name",
            Content = record,
        };

        v2.Value.Results.Add(result);
        var isUnitSaved = await CalculationUnitRepository.Update(v2.Value);
        Assert.That(isUnitSaved, Is.True);
    }
}
