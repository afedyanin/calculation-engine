using Hangfire;
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
    private readonly IBackgroundJobClient _backgroundJobs;

    public ReportsController(
        IMediator mediator,
        IBackgroundJobClient backgroundJobs)
    {
        _mediator = mediator;
        _backgroundJobs = backgroundJobs;
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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenerateReportParams request) 
    {
        var generateRequest = new GenerateReportRequest
        {
            ReportId = request.ReportId,
            AsOfDate = request.AsOfDate,
            AdditionalData = request.AdditionalData,
        };

        var jobId = _backgroundJobs.Enqueue(() => { _mediator.Send(generateRequest); });

        return Ok(jobId);
    }
}

public record GenerateReportParams
{
    public Guid ReportId { get; set; }

    public DateTime AsOfDate { get; set; }

    public string[] AdditionalData { get; set; } = Array.Empty<string>();
}
