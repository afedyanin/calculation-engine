using System.Collections.Concurrent;

namespace HfDemo.Application.Domain
{
    internal static class ReportInfoRepository
    {
        private static readonly ConcurrentDictionary<Guid, ReportInfo> _reports = new ConcurrentDictionary<Guid, ReportInfo>();

        public static ReportInfo? Get(Guid reportId)
        {
            _reports.TryGetValue(reportId, out var reportInfo);

            return reportInfo;
        }

        public static bool Upsert(ReportInfo reportInfo)
        {
            if (_reports.TryGetValue(reportInfo.ReportId, out var oldReportInfo))
            {
                return _reports.TryUpdate(reportInfo.ReportId, reportInfo, oldReportInfo);
            }

            return _reports.TryAdd(reportInfo.ReportId, reportInfo);
        }
    }
}
