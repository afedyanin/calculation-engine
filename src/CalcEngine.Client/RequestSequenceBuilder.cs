using CalcEngine.Client.Extensions;
using Hangfire;
using MediatR;

namespace CalcEngine.Client;

public class RequestSequenceBuilder
{
    private readonly IBackgroundJobClient _jobClient;

    private readonly IList<string> _jobIds;

    private string _lastJobId;

    public RequestSequenceBuilder(
        IBackgroundJobClient jobClient)
    {
        _jobClient = jobClient;
        _jobIds = [];
        _lastJobId = string.Empty;
    }

    public RequestSequenceBuilder WithRequest<T>(T request) where T : IRequest
    {
        if (string.IsNullOrEmpty(_lastJobId))
        {
            _lastJobId = _jobClient.EnqueueRequest(request);
            _jobIds.Add(_lastJobId);
            return this;
        }

        _lastJobId = _jobClient.ContinueWithRequest(_lastJobId, request);
        _jobIds.Add(_lastJobId);

        return this;
    }

    public string[] GetJobIds() => [.. _jobIds];

}
