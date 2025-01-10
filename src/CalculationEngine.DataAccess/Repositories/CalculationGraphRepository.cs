using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using CalculationEngine.DataAccess.Converters;
using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess.Repositories;

internal class CalculationGraphRepository : ICalculationGraphRepository
{
    private readonly IDbContextFactory<CalculationEngineDbContext> _contextFactory;

    public CalculationGraphRepository(IDbContextFactory<CalculationEngineDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<CalculationGraph?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

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

    public async Task<bool> Save(CalculationGraph graph, CancellationToken cancellationToken = default)
    {
        var graphDbo = graph.ToDbo();
        var units = graph.Vertices.Select(v => v.Value);

        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        await context.Graphs.AddAsync(graphDbo, cancellationToken);
        await context.CalculationUnits.AddRangeAsync(units, cancellationToken);

        var savedRecords = await context.SaveChangesAsync(cancellationToken);

        return savedRecords > 0;
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var graphDbo = await context
            .Graphs
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (graphDbo == null)
        {
            return false;
        }

        var unitIds = graphDbo.Vertices.Select(v => v.ValueId);
        if (unitIds.Any())
        {
            // TODO: Enshure calculation results is deleted
            var units = context.CalculationUnits.Where(u => unitIds.Contains(u.Id));
            context.CalculationUnits.RemoveRange(units);
        }

        context.Graphs.Remove(graphDbo);

        var savedRecords = await context.SaveChangesAsync(cancellationToken);

        return savedRecords > 0;
    }
}
