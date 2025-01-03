using HfDemo.Contracts.Entities;
using HfDemo.Contracts.Services;
using HfDemo.DomainModel;

namespace HfDemo.DataAccess;

public class CalcualtionJobRepository : IJobRepository
{
    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IJob> GetById(Guid jobId)
    {
        throw new NotImplementedException();
    }

    public Task<IJob> Save(IJob job)
    {
        throw new NotImplementedException();
    }

    public Task<IJob> SaveChildJob(string name, string type, string parameters)
    {
        throw new NotImplementedException();
    }

    public Task<IJobProgress> SaveProgress(int percent, string? message = null)
    {
        throw new NotImplementedException();
    }

    public Task<IJobResult> SaveResult(string name, string type, string resultJson, string? metadataJson, bool isCompleted, string? message)
    {
        throw new NotImplementedException();
    }
}
