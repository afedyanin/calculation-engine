using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;

namespace CalculationEngine.DataAccess.Repositories;

internal class CalculationUnitRepository : ICalculationUnitRepository
{
    public IEnumerable<CalculationUnit> GetByGraphId(Guid graphId)
    {
        throw new NotImplementedException();
    }

    public CalculationUnit? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CalculationResultItem> GetCalculationResults(Guid calculationUnitId)
    {
        throw new NotImplementedException();
    }

    public bool Save(CalculationUnit calculationUnit)
    {
        throw new NotImplementedException();
    }
}
