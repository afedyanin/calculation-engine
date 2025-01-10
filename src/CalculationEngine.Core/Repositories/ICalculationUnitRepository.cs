using CalculationEngine.Core.Model;

namespace CalculationEngine.Core.Repositories;
public interface ICalculationUnitRepository
{
    Task<CalculationUnit?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<CalculationUnit>> GetByGraphId(Guid graphId, CancellationToken cancellationToken = default);

    Task<IEnumerable<CalculationResultItem>> GetCalculationResults(Guid calculationUnitId, CancellationToken cancellationToken = default);

    Task<bool> Save(CalculationUnit calculationUnit, CancellationToken cancellationToken = default);
}
