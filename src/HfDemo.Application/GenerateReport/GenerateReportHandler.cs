using HfDemo.Application.Domain;
using MediatR;

namespace HfDemo.Application.GenerateReport;

internal class GenerateReportHandler : IRequestHandler<GenerateReportRequest, GenerateReportResponse>
{
    public async Task<GenerateReportResponse> Handle(GenerateReportRequest request, CancellationToken cancellationToken)
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

        reportInfo.Result = "Initialize calulation\n";
        reportInfo.Status = ReportStatus.InProgress;
        ReportInfoRepository.Upsert(reportInfo);

        for (int i = 0; i<=100; i++)
        {
            reportInfo.Result += $"Processing step #{i}\n";
            ReportInfoRepository.Upsert(reportInfo);

            await Task.Delay(1000);
        }

        return response;
    }
}
