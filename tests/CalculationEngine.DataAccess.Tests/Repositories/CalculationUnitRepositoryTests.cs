using CalculationEngine.Core.Model;

namespace CalculationEngine.DataAccess.Tests.Repositories;

[TestFixture(Category = "Database", Explicit = true)]
public class CalculationUnitRepositoryTests : DbTestBase
{
    [Test]
    public async Task CanSaveCalculationResultSet()
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
