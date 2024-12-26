using Hangfire.States;

namespace HfDemo.WebApi.JobManagement;

internal class CustomBackgroundJobStateChanger : IBackgroundJobStateChanger
{
    private readonly IBackgroundJobStateChanger _inner;

    public CustomBackgroundJobStateChanger(IBackgroundJobStateChanger inner)
    {
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
    }

    public IState ChangeState(StateChangeContext context)
    {
        Console.WriteLine($"ChangeState {context.BackgroundJobId} to {context.NewState}");
        return _inner.ChangeState(context);
    }
}
