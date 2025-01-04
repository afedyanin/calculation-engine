using MediatR;

namespace CalcEngine.Client.Internals;
internal class CalculationUnit : ICalculationUnit
{
    public ICalculationUnit? Parent { get; private set; }

    public IRequest Request { get; private set; }

    public string JobId { get; private set; }

    public IEnumerable<ICalculationUnit> Children { get; private set; }

    public CalculationUnit(IRequest request, ICalculationUnit? parent = null)
    {
        Request = request;
        Parent = parent;
    }
}
