namespace HfDemo.Contracts.Entities;

public interface IJobResult
{
    Guid Id { get; }

    Guid JobId { get; }

    DateTime CreationTime { get; }

    string Name { get; }

    string Type { get; }

    string? MetadataJson { get; }

    string ResultJson { get; }

    bool IsCompleted { get; }

    string? Message { get; }
}
