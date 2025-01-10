using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using CalculationEngine.DataAccess.Converters;
using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess.Repositories;

internal class CalculationGraphRepository : RepositoryBase, ICalculationGraphRepository
{
    public CalculationGraphRepository(IDbContextFactory<CalculationEngineDbContext> contextFactory)
        : base(contextFactory)
    {
    }

    public async Task<CalculationGraph?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = await GetDbContext();

        var graphDbo = await context
            .Graphs
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (graphDbo == null)
        {
            return null;
        }

        var graph = await graphDbo.FromDbo(context);
        return graph;
    }

    public async Task<bool> Insert(CalculationGraph graph, CancellationToken cancellationToken = default)
    {
        var graphDbo = graph.ToDbo();
        var units = graph.Vertices.Select(v => v.Value);

        using var context = await GetDbContext();

        context.Graphs.Add(graphDbo);
        context.CalculationUnits.AddRange(units);

        var savedRecords = await context.SaveChangesAsync(cancellationToken);

        return savedRecords > 0;
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = await GetDbContext();

        var graphDbo = await context.Graphs.FindAsync(id, cancellationToken);

        if (graphDbo == null)
        {
            return false;
        }

        var unitIds = graphDbo.Vertices.Select(v => v.ValueId);
        if (unitIds.Any())
        {
            var units = context.CalculationUnits.Where(u => unitIds.Contains(u.Id));
            context.CalculationUnits.RemoveRange(units);
            RemoveResultItems(unitIds, context);
        }

        context.Graphs.Remove(graphDbo);

        var savedRecords = await context.SaveChangesAsync(cancellationToken);

        return savedRecords > 0;
    }

    private void RemoveResultItems(IEnumerable<Guid> calculationUnitIds, CalculationEngineDbContext context)
    {
        foreach (var unitId in calculationUnitIds)
        {
            var resultItems = context.CalculationResultItems.Where(i => i.CalculationUnitId == unitId);
            if (resultItems.Any())
            {
                context.CalculationResultItems.RemoveRange(resultItems);
            }
        }
    }
}
