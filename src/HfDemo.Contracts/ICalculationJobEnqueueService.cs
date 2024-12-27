namespace HfDemo.Contracts;
public interface ICalculationJobEnqueueService
{
    string Enqueue(ICalculationRequest request, CancellationToken cancellationToken);
}
