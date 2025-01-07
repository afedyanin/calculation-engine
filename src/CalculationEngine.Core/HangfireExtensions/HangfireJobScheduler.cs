using CalculationEngine.Core.GraphModel;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using MediatR;

namespace CalculationEngine.Core.HangfireExtensions;

public class HangfireJobScheduler : IJobScheduler
{
    private readonly IBackgroundJobClient _jobClient;

    public HangfireJobScheduler(IBackgroundJobClient jobClient)
    {
        _jobClient = jobClient;
    }

    public string Enqueue(IRequest request)
    {
        return EnqueueBackgroundJob(_jobClient, request);
    }

    public string EnqueueAfter(string previousJobId, IRequest request)
    {
        return ContinueWithBackgropundJob(_jobClient, previousJobId, request);
    }

    // We should use generics here to store actual type of IRequest in Hangfire DB
    private static string EnqueueBackgroundJob<T>(
        IBackgroundJobClient client,
        T request) where T : IRequest
    {
        var job = CreateJob(request);
        return client.Create(job, new EnqueuedState());
    }

    private static string ContinueWithBackgropundJob<T>(
        IBackgroundJobClient client,
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
