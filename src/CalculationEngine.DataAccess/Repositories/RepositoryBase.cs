using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess.Repositories;

internal abstract class RepositoryBase
{
    private readonly IDbContextFactory<CalculationEngineDbContext> _contextFactory;

    protected Task<CalculationEngineDbContext> GetDbContext() => _contextFactory.CreateDbContextAsync();

    protected RepositoryBase(IDbContextFactory<CalculationEngineDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
