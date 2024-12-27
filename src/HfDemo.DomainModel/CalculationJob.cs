namespace HfDemo.DomainModel;

public class CalculationJob
{
    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public JobStatus Status { get; set; } = JobStatus.Unknown;

    public string ParametersJson { get; set; } = string.Empty;

    public IEnumerable<CalculationJob> ChildJobs { get; set; } = [];

    public IEnumerable<CalculationProgress> Progress { get; set; } = [];

    public IEnumerable<CalculationResult> Results { get; set; } = [];
}
