using MediatR;

namespace CalculationEngine.Core.Handlers;

internal class JobAwaitingHandler : IRequestHandler<JobAwaitingRequest>
{
    public Task Handle(JobAwaitingRequest request, CancellationToken cancellationToken)
    {
        // TODO: Проверить состояние джобов, переданных в реквесте
        // если все завершились - выключить рекаринг, чтобы текущая джоба попала в удаленные
        // удаление текущей джобы - сигнал для продвижения по графу
        return Task.CompletedTask;
    }
}
