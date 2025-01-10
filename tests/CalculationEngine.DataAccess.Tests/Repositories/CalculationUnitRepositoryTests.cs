namespace CalculationEngine.DataAccess.Tests.Repositories;

[TestFixture(Category = "Database", Explicit = true)]
public class CalculationUnitRepositoryTests : DbTestBase
{
    [Test]
    public async Task CanUpdateCalculationUnit()
    {
        var graph = GraphStubs.CreateSimpleGraph();
        var unitId = graph.Vertices[1].Value.Id;

        await CalculationGraphRepository.Insert(graph);

        var calculationUnit = await CalculationUnitRepository.GetById(unitId);
        Assert.That(calculationUnit, Is.Not.Null);

        calculationUnit.JobId = "Updated JobId";

        var isUnitSaved = await CalculationUnitRepository.Update(calculationUnit);
        Assert.That(isUnitSaved, Is.True);
    }
}
