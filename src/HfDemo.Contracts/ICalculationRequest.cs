using MediatR;

namespace HfDemo.Contracts;

public interface ICalculationRequest : IRequest
{
    Guid? Id { get; }

    string Name { get; }

    string Type { get; }

    string ParametersJson { get; }
}
