using MediatR;

namespace CalcEngine.Client;
public class CalcJobRequest : IRequest
{
    public Guid CorrelationId { get; set; }
}
