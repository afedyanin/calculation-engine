namespace HfDemo.DomainModel;

public class CalculationJob
{
    private readonly List<CalculationProgress> _progress = [];

    private readonly List<CalculationJob> _children = [];

    private readonly List<CalculationResult> _results = [];

    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public JobStatus Status { get; set; } = JobStatus.Unknown;

    public string ParametersJson { get; set; } = string.Empty;

    public IEnumerable<CalculationJob> ChildJobs => _children.AsReadOnly();

    public IEnumerable<CalculationProgress> Progress => _progress.AsReadOnly();

    public IEnumerable<CalculationResult> Results => _results.AsReadOnly();


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
