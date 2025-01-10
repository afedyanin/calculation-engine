using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess.Repositories;

internal class CalculationUnitRepository : RepositoryBase, ICalculationUnitRepository
{
    public CalculationUnitRepository(IDbContextFactory<CalculationEngineDbContext> contextFactory)
        : base(contextFactory)
    {
    }

    public async Task<CalculationUnit?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = await GetDbContext();

        var calculationUnit = await context
            .CalculationUnits
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return calculationUnit;
    }

    public async Task<IEnumerable<CalculationUnit>> GetByGraphId(Guid graphId, CancellationToken cancellationToken = default)
    {
        using var context = await GetDbContext();

        var calculationUnits = await context
            .CalculationUnits
            .AsNoTracking()
            .Where(x => x.GraphId == graphId)
            .ToArrayAsync(cancellationToken);

        return calculationUnits;
    }

    public async Task<bool> Update(CalculationUnit calculationUnit, CancellationToken cancellationToken = default)
    {
        using var context = await GetDbContext();

        var entry = context.CalculationUnits.Update(calculationUnit);

        var savedRecords = await context.SaveChangesAsync(cancellationToken);

        return savedRecords > 0;
    }
}
