using HfDemo.Contracts.Entities;

namespace HfDemo.Contracts.Services;

public interface IJobRepository
{
    Task<IJob> GetById(Guid jobId);

    Task<IJob> Save(IJob job);

    Task<bool> Delete(Guid id);

    Task<IJobProgress> SaveProgress(int percent, string? message = null);

    Task<IJob> SaveChildJob(string name, string type, string parameters);

    Task<IJobResult> SaveResult(string name, string type, string resultJson, string? metadataJson, bool isCompleted, string? message);
}
