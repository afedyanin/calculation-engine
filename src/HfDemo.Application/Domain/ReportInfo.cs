namespace HfDemo.Application.Domain
{
    public class ReportInfo
    {
        public Guid ReportId { get; set; }

        public DateTime AsOfDate { get; set; }

        public string[] AdditionalData { get; set; } = Array.Empty<string>();

        public ReportStatus Status { get; set; }

        public string Result { get; set; } = string.Empty;
    }

    public enum ReportStatus
    {
        Unknown = 0,
        Initial = 1,
        InProgress = 2,
        Success = 3,
        Error = 4
    }
}
