using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess.Repositories;

internal class CalculationUnitRepository : ICalculationUnitRepository
{
    private readonly IDbContextFactory<CalculationEngineDbContext> _contextFactory;

    public CalculationUnitRepository(IDbContextFactory<CalculationEngineDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<CalculationUnit?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var calculationUnit = await context.CalculationUnits.FindAsync(id);

        return calculationUnit;
    }

    public async Task<IEnumerable<CalculationUnit>> GetByGraphId(Guid graphId, CancellationToken cancellationToken = default)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var calculationUnits = await context
            .CalculationUnits
            //.AsNoTracking()
            .Where(x => x.GraphId == graphId)
            .ToArrayAsync(cancellationToken);

        return calculationUnits;
    }

    public async Task<IEnumerable<CalculationResultItem>> GetCalculationResults(Guid calculationUnitId, CancellationToken cancellationToken = default)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var calculationResults = await context
            .CalculationResultItems
            .AsNoTracking()
            .Where(x => x.CalculationUnitId == calculationUnitId)
            .ToArrayAsync(cancellationToken);

        return calculationResults;
    }

    public async Task<bool> Update(CalculationUnit calculationUnit, CancellationToken cancellationToken = default)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        //context.CalculationUnits.Add(calculationUnit);
        context.CalculationResultItems.AddRange(calculationUnit.Results);

        var savedRecords = await context.SaveChangesAsync(cancellationToken);

        return savedRecords > 0;
    }
}
