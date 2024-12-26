using HfDemo.Application.ReportStatus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HfDemo.Application.GetReportStatus;

public class GetReportStatusRequest : IRequest<GetReportStatusResponse>
{
    public Guid ReportId { get; set; }
}
