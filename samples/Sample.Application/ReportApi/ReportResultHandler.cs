using CalculationEngine.Core.Repositories;
using MediatR;
using Sample.Application.ReportModel;

namespace Sample.Application.ReportApi;

public class ReportResultHandler : IRequestHandler<ReportResultRequest, ReportResultResponse>
{
    private readonly ICalculationResultRepository _calculationResultRepository;

    public ReportResultHandler(ICalculationResultRepository calculationResultRepository)
    {
        _calculationResultRepository = calculationResultRepository;
    }

    public async Task<ReportResultResponse> Handle(ReportResultRequest request, CancellationToken cancellationToken)
    {
        var unitId = request.ReportResultCalculationUnitId;

        var results = await _calculationResultRepository.GetByCalculationUnitId(unitId);

        if (results == null)
        {
            return new ReportResultResponse
            {
                IsReady = false,
            };
        }

        var reportData = results.FirstOrDefault();

        if (reportData == null)
        {
            return new ReportResultResponse
            {
                IsReady = false,
            };
        }

        var reportDataItems = reportData.Content as ReportDataItem[];

        return new ReportResultResponse
        {
            IsReady = true,
            ReportData = reportDataItems ?? []
        };
    }
}
