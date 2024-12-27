using HfDemo.Contracts.Abstractions;

namespace HfDemo.Contracts.Entities;

public interface IJob
{
    Guid Id { get; }

    Guid? ParentId { get; }

    string Name { get; }

    string Type { get; }

    DateTime StartTime { get; }

    DateTime? EndTime { get; }

    JobStatus Status { get; }

    string ParametersJson { get; }

    IEnumerable<IJob> ChildJobs { get; }

    IEnumerable<IJobProgress> Progress { get; }

    IEnumerable<IJobResult> Results { get; }
}
