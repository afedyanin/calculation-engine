namespace HfDemo.DomainModel;

public class CalculationProgress
{
    public Guid Id { get; set; }

    public Guid JobId { get; set; }

    public DateTime CreationTime { get; set; }

    public int Prercent { get; set; }

    public string? Message { get; set; }
}
