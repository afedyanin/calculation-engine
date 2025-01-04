using MediatR;

namespace CalcEngine.Client;

public interface ICalculationUnit
{
    ICalculationUnit? Parent { get; }

    IRequest Request { get; }

    string JobId { get; }

    IEnumerable<ICalculationUnit> Children { get; }
}
