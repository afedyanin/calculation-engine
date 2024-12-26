using HfDemo.Application.Domain;
using MediatR;

namespace HfDemo.Application.GetReportResult
{
    internal class GetReportResultHandler : IRequestHandler<GetReportResultRequest, GetReportResultResponse>
    {
        public Task<GetReportResultResponse> Handle(GetReportResultRequest request, CancellationToken cancellationToken)
        {
            var reportInfo = ReportInfoRepository.Get(request.ReportId);

            if (reportInfo == null)
            {
                return Task.FromResult(GetReportResultResponse.EmptyResult);
            }

            var response = new GetReportResultResponse()
            {
                ReportId = reportInfo.ReportId,
                ReportResult = reportInfo.Result
            };

            return Task.FromResult(response);
        }
    }
}
