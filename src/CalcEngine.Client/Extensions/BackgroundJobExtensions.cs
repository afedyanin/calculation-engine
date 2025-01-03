using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using MediatR;

namespace CalcEngine.Client.Extensions;
public static class BackgroundJobExtensions
{
    public static string EnqueueRequest<T>(
        this IBackgroundJobClient client,
        T request) where T : IRequest
    {
        var job = CreateJob(request);
        return client.Create(job, new EnqueuedState());
    }

    public static string ContinueWithRequest<T>(
        this IBackgroundJobClient client,
        string parentId,
        T request) where T : IRequest
    {
        var job = CreateJob(request);
        return client.Create(job, new AwaitingState(parentId));
    }

    private static Job CreateJob<T>(T request) where T : IRequest
    {
        // CancellationToken type instances are substituted with ShutdownToken 
        // during the background job performance, so we don't need to store 
        // their values.
        var job = Job.FromExpression<IMediator>(m => m.Send(request, CancellationToken.None));
        return job;
    }
}
