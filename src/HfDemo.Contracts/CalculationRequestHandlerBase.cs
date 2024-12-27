using MediatR;

namespace HfDemo.Contracts;

public abstract class CalculationRequestHandlerBase : IRequestHandler<ICalculationRequest>
{
    public abstract Task Handle(ICalculationRequest request, CancellationToken cancellationToken);

    // Create Job
    // Create Shild Job(s)
    // Create results
    // Create progress
    // Save Job
}
