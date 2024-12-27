namespace HfDemo.Contracts.Services;
public interface IJobEnqueueService
{
    string Enqueue(ICalculationRequest request, CancellationToken cancellationToken);
}
