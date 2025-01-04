using CalcEngine.Client.Internals;
using MediatR;

namespace CalcEngine.Client;

internal class CalculationGraph
{
    private readonly ICalculationUnit? _root;

    public Guid Id { get; private set; }

    public CalculationGraph(IRequest request)
    {
        Id = Guid.NewGuid();
        _root = new CalculationUnit(request);
    }

    public void AppendParallel(ICalculationUnit parent, params IRequest[] requests)
    {
    }

    public void AppendSequential(ICalculationUnit parent, params IRequest[] requests)
    {
    }

    public string Render()
    {
        return string.Empty;
    }

    public void Execute()
    {
        // поставить в очередь все задачи
        // присвоить jobId всем нодам
    }

    // TODO: реализовать стандартный итератор прохода по дереву
}
