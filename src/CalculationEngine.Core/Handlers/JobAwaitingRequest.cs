namespace CalculationEngine.Core.Handlers;

public class JobAwaitingRequest : ICalculationRequest
{
    private static readonly TimeSpan _defaultPoolingInterval = TimeSpan.FromSeconds(10);

    public Guid CalculationUnitId { get; set; }

    public string[] JobIds { get; set; } = [];

    public TimeSpan PoolingInterval { get; set; } = _defaultPoolingInterval;
}
