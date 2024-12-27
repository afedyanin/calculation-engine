using HfDemo.Contracts.Entities;

namespace HfDemo.Contracts.Services;

public interface IJobFactory
{
    IJob CreateFromRequest(ICalculationRequest request);
}
