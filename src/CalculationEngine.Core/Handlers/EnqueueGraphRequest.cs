using MediatR;

namespace CalculationEngine.Core.Handlers;

public class EnqueueGraphRequest : IRequest<EnqueueGraphResponse>
{
    public Guid GraphId { get; set; }
}
