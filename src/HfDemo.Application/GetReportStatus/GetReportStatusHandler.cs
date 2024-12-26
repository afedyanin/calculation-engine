using HfDemo.Application.ReportStatus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HfDemo.Application.GetReportStatus
{
    internal class GetReportStatusHandler : IRequestHandler<GetReportStatusRequest, GetReportStatusResponse>
    {
        public Task<GetReportStatusResponse> Handle(GetReportStatusRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
