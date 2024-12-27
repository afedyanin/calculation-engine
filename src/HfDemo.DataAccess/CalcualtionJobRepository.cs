using HfDemo.DomainModel;
using HfDemo.DomainModel.Abstractions;

namespace HfDemo.DataAccess;

public class CalcualtionJobRepository : ICalculationJobRepository
{
    public CalculationJob GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CalculationJob> GetByParentId(Guid parentId)
    {
        throw new NotImplementedException();
    }

    public bool Upsert(CalculationJob job)
    {
        throw new NotImplementedException();
    }
    public bool Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CalculationResult> GetResults(Guid jobId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CalculationProgress> GetProgress(Guid jobId)
    {
        throw new NotImplementedException();
    }
}
