using CalculationEngine.Core.Model;

namespace CalculationEngine.Core.Repositories;
public interface ICalculationResultRepository
{

    Task<CalculationResultItem?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<CalculationResultItem>> GetByCalculationUnitId(Guid calculationUnitId, CancellationToken cancellationToken = default);

    Task<bool> Insert(CalculationResultItem calculationResultItem, CancellationToken cancellationToken = default);

    Task<bool> Update(CalculationResultItem calculationResultItem, CancellationToken cancellationToken = default);

    Task<bool> Delete(Guid resultItemId, CancellationToken cancellationToken = default);
}
