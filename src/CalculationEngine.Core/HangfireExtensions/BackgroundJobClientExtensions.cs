using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using MediatR;

namespace CalculationEngine.Core.HangfireExtensions;
public static class BackgroundJobClientExtensions
{
    public static string EnqueueBackgroundJob<T>(
        this IBackgroundJobClient client,
        T request) where T : IRequest
    {
        var job = CreateJob(request);
        return client.Create(job, new EnqueuedState());
    }

    public static string ContinueWithBackgropundJob<T>(
        this IBackgroundJobClient client,
        string parentJobId,
        T request) where T : IRequest
    {
        var job = CreateJob(request);
        return client.Create(job, new AwaitingState(parentJobId));
    }

    private static Job CreateJob<T>(T request) where T : IRequest
    {
        // CancellationToken type instances are substituted with ShutdownToken 
        // during the background job performance, so we don't need to store their values.
        var job = Job.FromExpression<IMediator>(m => m.Send(request, CancellationToken.None));
        return job;
    }
}
