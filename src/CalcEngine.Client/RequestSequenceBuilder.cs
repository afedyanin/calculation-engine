using CalcEngine.Client.Extensions;
using Hangfire;
using MediatR;

namespace CalcEngine.Client;

public class RequestSequenceBuilder
{
    private readonly IBackgroundJobClient _jobClient;
    private readonly LinkedList<string> _jobIds;

    public RequestSequenceBuilder(
        IBackgroundJobClient jobClient)
    {
        _jobClient = jobClient;
        _jobIds = [];
    }

    public RequestSequenceBuilder WithRequest<T>(T request) where T : IRequest
    {
        var last = _jobIds.LastOrDefault();

        if (string.IsNullOrEmpty(last))
        {
            last = _jobClient.EnqueueRequest(request);
            _jobIds.AddLast(last);
            return this;
        }

        last = _jobClient.ContinueWithRequest(last, request);
        _jobIds.AddLast(last);

        return this;
    }

    public string[] GetJobIds() => [.. _jobIds];

}
