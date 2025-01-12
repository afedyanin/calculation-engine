using MediatR;

namespace CalculationEngine.Core.Handlers;

public class EnqueueGraphRequest : IRequest
{
    public Guid GraphId { get; set; }
}
