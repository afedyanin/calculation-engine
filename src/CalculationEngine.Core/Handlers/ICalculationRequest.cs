using MediatR;

namespace CalculationEngine.Core.Handlers;
public interface ICalculationRequest : IRequest
{
    Guid CalculationUnitId { get; set; }
}
