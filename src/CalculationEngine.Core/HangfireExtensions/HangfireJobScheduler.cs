using CalculationEngine.Core.GraphModel;
using Hangfire;
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
        return _jobClient.EnqueueBackgroundJob(request);
    }

    public string EnqueueAfter(string previousJobId, IRequest request)
    {
        return _jobClient.ContinueWithBackgropundJob(previousJobId, request);
    }
}
