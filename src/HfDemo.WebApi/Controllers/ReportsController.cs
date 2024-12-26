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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenerateReportParams request) 
    {
        var newId = Guid.NewGuid();

        var generateRequest = new GenerateReportRequest
        {
            ReportId = newId,
            AsOfDate = request.AsOfDate,
            AdditionalData = request.AdditionalData,
        };

        var res = await _mediator.Send(generateRequest);

        return Ok(res);
    }
}

public record GenerateReportParams
{
    public DateTime AsOfDate { get; set; }

    public string[] AdditionalData { get; set; } = Array.Empty<string>();
}
