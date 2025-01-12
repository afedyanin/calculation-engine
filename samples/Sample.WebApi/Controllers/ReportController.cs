using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.ReportApi;

namespace Sample.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReportController> _logger;

    public ReportController(
        IMediator mediator,
        ILogger<ReportController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<EnqueueReportResponse> Post()
    {
        var request = new EnqueueReportRequest();

        var response = await _mediator.Send(request);

        if (response.Success)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"GraphId={response.GraphId} enqueued with JobId={response.JobId}");
            sb.AppendLine($"ResultId={response.ReportResultCalculationUnitId}");
            sb.AppendLine(response.GraphVerticesTraversal);
            _logger.LogInformation(sb.ToString());
        }
        else
        {
            _logger.LogError($"Enqueue error: {response.Message}");
        }

        return response;
    }


    [HttpGet("{id:guid}")]
    public async Task<ReportResultResponse> Get(Guid id)
    {
        var request = new ReportResultRequest()
        {
            ReportResultCalculationUnitId = id
        };

        var response = await _mediator.Send(request);

        return response;
    }
}
