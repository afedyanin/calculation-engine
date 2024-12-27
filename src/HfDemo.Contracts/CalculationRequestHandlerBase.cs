using HfDemo.Contracts.Entities;
using HfDemo.Contracts.Services;
using MediatR;

namespace HfDemo.Contracts;

public abstract class CalculationRequestHandlerBase<T> : IRequestHandler<T> where T : ICalculationRequest
{
    private readonly IJobRepository _jobRepository;
    private readonly IJobFactory _jobFactory;

    protected IJob CurrentJob { get; private set; }

    protected CalculationRequestHandlerBase(
        IJobRepository jobRepository,
        IJobFactory jobFactory)
    {
        _jobRepository = jobRepository;
        _jobFactory = jobFactory;
    }

    public virtual async Task Handle(T request, CancellationToken cancellationToken)
    {
        CurrentJob = await InitCurrentJob(request);
    }

    protected virtual async Task<IJob> InitCurrentJob(T request)
    {
        var job = _jobFactory.CreateFromRequest(request);
        return await _jobRepository.Save(job);
    }

    protected virtual Task<IJob> SaveCurrentJob() => _jobRepository.Save(CurrentJob);

    protected virtual Task<IJobProgress> SaveProgress(
        int percent,
        string? message = null) => _jobRepository.SaveProgress(percent, message);
    protected virtual Task<IJobResult> SaveResult(
        string name,
        string type,
        string resultJson,
        string? metadataJson,
        bool isCompleted,
        string? message) => _jobRepository.SaveResult(name, type, resultJson, metadataJson, isCompleted, message);

    protected virtual Task<IJob> SaveChildJob(
        string name,
        string type,
        string parameters) => _jobRepository.SaveChildJob(name, type, parameters);
}
