using HfDemo.Application.Domain;
using MediatR;

namespace HfDemo.Application.GetReportStatus
{
    internal class GetReportStatusHandler : IRequestHandler<GetReportStatusRequest, GetReportStatusResponse>
    {
        public Task<GetReportStatusResponse> Handle(GetReportStatusRequest request, CancellationToken cancellationToken)
        {
            var reportInfo = ReportInfoRepository.Get(request.ReportId);

            if (reportInfo == null)
            {
                return Task.FromResult(GetReportStatusResponse.EmptyStatus);
            }

            var response = new GetReportStatusResponse()
            {
                ReportId = reportInfo.ReportId,
                Status = reportInfo.Status,
            };

            return Task.FromResult(response);
        }
    }
}
