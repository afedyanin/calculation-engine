using HfDemo.Application.Domain;
using MediatR;

namespace HfDemo.Application.GenerateReport;

internal class GenerateReportHandler : IRequestHandler<GenerateReportRequest, GenerateReportResponse>
{
    public Task<GenerateReportResponse> Handle(GenerateReportRequest request, CancellationToken cancellationToken)
    {
        var reportInfo = new ReportInfo 
        { 
            ReportId = request.ReportId,
            AsOfDate = request.AsOfDate,
            Status = ReportStatus.Initial,
            AdditionalData = request.AdditionalData,
        };

        ReportInfoRepository.Upsert(reportInfo);

        var response = new GenerateReportResponse
        {
            ReportId = reportInfo.ReportId,
            Status = reportInfo.Status,
        };

        // TODO: Emulate report calculation


        return Task.FromResult(response);
    }
}
