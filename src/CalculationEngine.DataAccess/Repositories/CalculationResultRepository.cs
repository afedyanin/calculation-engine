using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess.Repositories;
internal class CalculationResultRepository : RepositoryBase, ICalculationResultRepository
{
    public CalculationResultRepository(IDbContextFactory<CalculationEngineDbContext> contextFactory)
        : base(contextFactory)
    {
    }
    public async Task<CalculationResultItem?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = await GetDbContext();

        var calculationResult = await context
            .CalculationResultItems
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return calculationResult;
    }

    public async Task<IEnumerable<CalculationResultItem>> GetByCalculationUnitId(Guid calculationUnitId, CancellationToken cancellationToken = default)
    {
        using var context = await GetDbContext();

        var calculationResults = await context
            .CalculationResultItems
            .AsNoTracking()
            .Where(x => x.CalculationUnitId == calculationUnitId)
            .ToArrayAsync(cancellationToken);

        return calculationResults;
    }

    public async Task<bool> Insert(CalculationResultItem calculationResultItem, CancellationToken cancellationToken = default)
    {
        using var context = await GetDbContext();

        var entry = context.CalculationResultItems.Add(calculationResultItem);

        var savedRecords = await context.SaveChangesAsync(cancellationToken);

        return savedRecords > 0;
    }

    public async Task<bool> Update(CalculationResultItem calculationResultItem, CancellationToken cancellationToken = default)
    {
        using var context = await GetDbContext();

        var entry = context.CalculationResultItems.Update(calculationResultItem);

        var savedRecords = await context.SaveChangesAsync(cancellationToken);

        return savedRecords > 0;
    }
    public async Task<bool> Delete(Guid resultItemId, CancellationToken cancellationToken = default)
    {
        using var context = await GetDbContext();

        var calculationResultItem = await context.CalculationResultItems.FindAsync(resultItemId);

        if (calculationResultItem == null)
        {
            return false;
        }

        var entry = context.CalculationResultItems.Remove(calculationResultItem);

        var savedRecords = await context.SaveChangesAsync(cancellationToken);

        return savedRecords > 0;
    }
}
