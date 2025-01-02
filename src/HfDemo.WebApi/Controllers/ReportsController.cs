using HfDemo.Application.Domain;
using HfDemo.Application.GenerateReport;
using HfDemo.Application.GetReportResult;
using HfDemo.Application.GetReportStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HfDemo.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public IActionResult Post([FromBody] GenerateReportParams request)
    {
        var reportRequest = new GenerateReportRequest();
        var res = _mediator.Send(reportRequest);
        return Ok(res);
    }

    // TODO: Remove it
    [HttpGet]
    public IActionResult Get()
    {
        var res = ReportInfoRepository.Get();

        return Ok(res);
    }

    [HttpGet("{id:guid}/status")]
    public async Task<IActionResult> GetStatus(Guid id)
    {
        var request = new GetReportStatusRequest()
        {
            ReportId = id
        };

        var res = await _mediator.Send(request);

        return Ok(res); 
    }

    [HttpGet("{id:guid}/result")]
    public async Task<IActionResult> GetResult(Guid id)
    {
        var request = new GetReportResultRequest()
        {
            ReportId = id
        };

        var res = await _mediator.Send(request);

        return Ok(res);
    }

}

public record GenerateReportParams
{
    public Guid ReportId { get; set; }

    public DateTime AsOfDate { get; set; }

    public string[] AdditionalData { get; set; } = Array.Empty<string>();
}
