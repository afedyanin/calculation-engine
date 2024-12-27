using HfDemo.Contracts.Entities;

namespace HfDemo.DomainModel;

public class CalculationResult : IJobResult
{
    public Guid Id { get; set; }

    public Guid JobId { get; set; }

    public DateTime CreationTime { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string? MetadataJson { get; set; }

    public string ResultJson { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public string? Message { get; set; }
}
