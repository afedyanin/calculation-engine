namespace CalculationEngine.Core.Handlers;

public class JobAwaitingRequest : ICalculationRequest
{
    public Guid CalculationUnitId { get; set; }

    public string[] JobIds { get; set; } = [];

    public TimeSpan PoolingInterval { get; set; }
}
