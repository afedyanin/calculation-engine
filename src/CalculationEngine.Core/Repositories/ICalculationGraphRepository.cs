using CalculationEngine.Core.Model;

namespace CalculationEngine.Core.Repositories;

public interface ICalculationGraphRepository
{
    CalculationGraph? GetById(Guid id);

    bool Save(CalculationGraph graph);

    bool Delete(Guid id);
}
