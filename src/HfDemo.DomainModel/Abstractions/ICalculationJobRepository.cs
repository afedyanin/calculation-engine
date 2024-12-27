namespace HfDemo.DomainModel.Abstractions;

public interface ICalculationJobRepository
{
    CalculationJob GetById(Guid jobId);

    IEnumerable<CalculationJob> GetByParentId(Guid parentJobId);

    IEnumerable<CalculationResult> GetResults(Guid jobId);

    IEnumerable<CalculationProgress> GetProgress(Guid jobId);

    bool Upsert(CalculationJob job);

    bool Delete(Guid id);
}
