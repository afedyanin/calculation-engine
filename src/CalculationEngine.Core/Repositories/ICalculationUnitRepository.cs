using CalculationEngine.Core.Model;

namespace CalculationEngine.Core.Repositories;
public interface ICalculationUnitRepository
{
    CalculationUnit? GetById(Guid id);

    IEnumerable<CalculationUnit> GetByGraphId(Guid graphId);

    IEnumerable<CalculationResultItem> GetCalculationResults(Guid calculationUnitId);

    bool Save(CalculationUnit calculationUnit);
}
