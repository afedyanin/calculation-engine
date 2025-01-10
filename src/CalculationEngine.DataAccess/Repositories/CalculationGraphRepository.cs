using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;

namespace CalculationEngine.DataAccess.Repositories;

internal class CalculationGraphRepository : ICalculationGraphRepository
{
    public Task<CalculationGraph?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Save(CalculationGraph graph, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
