using MediatR;

namespace CalculationEngine.Core.Handlers;
public interface ICalculationRequest : IRequest
{
    Guid NodeId { get; }
}
