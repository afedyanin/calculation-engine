using HfDemo.Contracts.Abstractions;
using HfDemo.Contracts.Entities;

namespace HfDemo.DomainModel;

public class CalculationJob : IJob
{
    private readonly List<IJobProgress> _progress = [];

    private readonly List<IJob> _children = [];

    private readonly List<IJobResult> _results = [];

    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public JobStatus Status { get; set; } = JobStatus.Unknown;

    public string ParametersJson { get; set; } = string.Empty;

    public IEnumerable<IJob> ChildJobs => _children.AsReadOnly();

    public IEnumerable<IJobProgress> Progress => _progress.AsReadOnly();

    public IEnumerable<IJobResult> Results => _results.AsReadOnly();


    public void AppendProgress(int percent, string? message = null)
    {
        var progress = new CalculationProgress
        {
            Id = Guid.NewGuid(),
            CreationTime = DateTime.UtcNow,
            JobId = Id,
            Prercent = percent,
            Message = message
        };

        _progress.Add(progress);
    }

    public CalculationJob CreateChildJob(string name, string type, string parameters)
    {
        var job = new CalculationJob
        {
            Id = Guid.NewGuid(),
            ParentId = Id,
            Name = name,
            Type = type,
            StartTime = DateTime.UtcNow,
            Status = JobStatus.Unknown,
            ParametersJson = parameters
        };

        _children.Add(job);
        return job;
    }

    public void SetResult(string name, string type, string resultJson, string? metadataJson, bool isCompleted, string? message)
    {
        var result = new CalculationResult
        {
            Id = Guid.NewGuid(),
            CreationTime = DateTime.UtcNow,
            JobId = Id,
            Name = name,
            Type = type,
            ResultJson = resultJson,
            MetadataJson = metadataJson,
            IsCompleted = isCompleted,
            Message = message
        };

        _results.Add(result);
    }
}
