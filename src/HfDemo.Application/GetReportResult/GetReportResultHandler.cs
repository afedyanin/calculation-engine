using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HfDemo.Application.GetReportResult
{
    internal class GetReportResultHandler : IRequestHandler<GetReportResultRequest, GetReportResultResponse>
    {
        public Task<GetReportResultResponse> Handle(GetReportResultRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
