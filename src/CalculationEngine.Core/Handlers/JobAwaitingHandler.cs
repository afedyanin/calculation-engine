using Hangfire;
using Hangfire.Storage;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalculationEngine.Core.Handlers;

internal class JobAwaitingHandler : IRequestHandler<JobAwaitingRequest>
{
    private static readonly Dictionary<string, bool> _stateDict =
        new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase)
        {
            { "Awaiting", false },
            { "Deleted", true },
            { "Enqueued", false },
            { "Failed", true },
            { "Processing", false },
            { "Scheduled", false },
            { "Succeeded", true },
        };

    private readonly ILogger<JobAwaitingHandler> _logger;
    private readonly IMonitoringApi _monitoringApi;

    public JobAwaitingHandler(ILogger<JobAwaitingHandler> logger)
    {
        _logger = logger;
        _monitoringApi = JobStorage.Current.GetMonitoringApi();
    }

    public async Task Handle(JobAwaitingRequest request, CancellationToken cancellationToken)
    {
        if (request.JobIds.Length == 0)
        {
            _logger.LogError($"Awating job has no any job to wait. CalculationUnitId={request.CalculationUnitId}");
            return;
        }

        var jobIds = string.Join(',', request.JobIds);
        _logger.LogDebug($"Start waiting for jobs completed. JobIds=[{jobIds}]");

        while (!AllJobsCompleted(request.JobIds))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogWarning($"Cancel waiting for jobs completed. JobIds=[{jobIds}]");
                break;
            }

            _logger.LogDebug($"Continue waiting for jobs completed. JobIds=[{jobIds}]");
            await Task.Delay(request.PoolingInterval, cancellationToken);
        }

        _logger.LogInformation($"End waiting for jobs completed. JobIds=[{jobIds}]. Proceed to next job.");
    }

    private bool AllJobsCompleted(string[] jobIds)
        => jobIds.All(jobId => IsCompleted(GetLastState(jobId)));

    private string GetLastState(string jobId)
    {
        if (string.IsNullOrEmpty(jobId))
        {
            return string.Empty;
        }

        var jobDeatils = _monitoringApi.JobDetails(jobId);

        if (jobDeatils == null)
        {
            return string.Empty;
        }

        var lastState = jobDeatils.History.OrderBy(x => x.CreatedAt).LastOrDefault();

        if (lastState == null)
        {
            return string.Empty;
        }

        return lastState.StateName;
    }
    private static bool IsCompleted(string? stateName)
    {
        if (string.IsNullOrEmpty(stateName))
        {
            return false;
        }

        if (!_stateDict.TryGetValue(stateName, out var isCompleted))
        {
            return false;
        }

        return isCompleted;
    }
}
