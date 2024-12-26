using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace HfDemo.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private static readonly ConcurrentDictionary<Guid, GenerateReportParams> _reports = new ConcurrentDictionary<Guid, GenerateReportParams>();

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        if (_reports.TryGetValue(id, out var report))
        { 
            return Ok(report); 
        }

        return NotFound();
    }

    [HttpPut("{id:guid}/status")]
    public IActionResult Put(Guid id, [FromBody] string status) 
    {
        if (_reports.TryGetValue(id, out var report))
        {
            report.Status = status;
            return Ok(report);
        }

        return NotFound();
    }

    [HttpPost]
    public IActionResult Post([FromBody] GenerateReportParams request) 
    {
        var newId = Guid.NewGuid();
        _reports.TryAdd(newId, request);
        return Ok(newId);
    }
}

public record GenerateReportParams
{
    public string Status { get; set; } = string.Empty;
    
    public Guid ReportId { get; set; }

    public DateTime AsOfDate { get; set; }

    public string[] AdditionalData { get; set; } = Array.Empty<string>();
}
