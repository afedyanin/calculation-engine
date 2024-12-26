using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HfDemo.Application.GetReportResult;

public class GetReportResultRequest : IRequest<GetReportResultResponse>
{
    public Guid ReportId { get; set; }
}
