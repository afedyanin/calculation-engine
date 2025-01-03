using System.Reflection;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using MediatR;

namespace CalcEngine.Client.Extensions;
public static class BackgroundJobExtensions
{
    public static IEnumerable<string> EnqueueSequence(
        this IBackgroundJobClient client,
        params IRequest[] requests)
    {
        var jobIds = new List<string>();
        var jobId = string.Empty;

        foreach (var request in requests)
        {
            // first job
            if (string.IsNullOrEmpty(jobId))
            {
                jobId = client.EnqueueMediatorRequest(request);
                jobIds.Add(jobId);
                continue;
            }

            jobId = client.ContinueJobWithMediatorRequest(jobId, request);
            jobIds.Add(jobId);
        }

        return jobIds;
    }

    public static string EnqueueMediatorRequest<T>(
        this IBackgroundJobClient client,
        T request) where T : IRequest
    {
        var job = CreateJob(request);
        return client.Create(job, new EnqueuedState());
    }

    public static string ContinueJobWithMediatorRequest<T>(
        this IBackgroundJobClient client,
        string parentId,
        T request) where T : IRequest
    {
        var job = CreateJob(request);
        return client.Create(job, new AwaitingState(parentId));
    }

    public static Job CreateJob<T>(T request) where T : IRequest
    {
        var methodInfo = GetMediatorMethodInfo<T>();
        return new Job(methodInfo, request, CancellationToken.None);
    }

    private static MethodInfo GetMediatorMethodInfo<T>() where T : IRequest
    {
        var methodInfo = typeof(Mediator)
            .GetMethods()
            .First(
            x => x.Name.Equals(nameof(Mediator.Send),
                StringComparison.OrdinalIgnoreCase)
            && x.IsGenericMethod
            && x.GetParameters().Length == 2);

        var genMethod = methodInfo.MakeGenericMethod(typeof(T));
        return genMethod;
    }
}
