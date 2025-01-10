using CalculationEngine.Core.Model;

namespace CalculationEngine.Core.Repositories;

public interface ICalculationGraphRepository
{
    Task<CalculationGraph?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<bool> Insert(CalculationGraph graph, CancellationToken cancellationToken = default);

    Task<bool> Delete(Guid id, CancellationToken cancellationToken = default);
}
