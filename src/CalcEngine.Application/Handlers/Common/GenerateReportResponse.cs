namespace CalcEngine.Application.Handlers.Common;
public class GenerateReportResponse
{
    public Guid CorrelationId { get; set; }
    public IEnumerable<string> JobIds { get; set; } = [];
}
