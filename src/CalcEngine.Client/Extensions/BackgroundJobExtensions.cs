using Hangfire;
using MediatR;

namespace CalcEngine.Client.Extensions;
public static class BackgroundJobExtensions
{
    public static IEnumerable<string> EnqueueSequence(
        this IBackgroundJobClient client,
        IEnumerable<IRequest> requests,
        CancellationToken cancellationToken = default)
    {
        var jobIds = new List<string>();
        var jobId = string.Empty;

        foreach (var request in requests)
        {
            // first job
            if (string.IsNullOrEmpty(jobId))
            {
                jobId = client.Enqueue<IMediator>(m => m.Send(request, cancellationToken));
                jobIds.Add(jobId);
                continue;
            }
            jobId = client.ContinueJobWith<IMediator>(jobId, m => m.Send(request, cancellationToken));
            jobIds.Add(jobId);
        }

        return jobIds;
    }
}
