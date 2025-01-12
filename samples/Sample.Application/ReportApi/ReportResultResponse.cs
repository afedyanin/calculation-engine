using Sample.Application.ReportModel;

namespace Sample.Application.ReportApi;

public class ReportResultResponse
{
    public bool IsReady { get; set; }

    public IEnumerable<ReportDataItem> ReportData { get; set; } = [];
}
