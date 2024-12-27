namespace HfDemo.Contracts.Entities;
public interface IJobProgress
{
    Guid Id { get; }

    Guid JobId { get; }

    DateTime CreationTime { get; }

    int Prercent { get; }

    string? Message { get; }
}
