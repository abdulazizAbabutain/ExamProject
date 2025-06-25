using Serilog.Events;

namespace Domain.Auditing
{
    public class ApplicationLog
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
        public LogEventLevel Level { get; set; }
        public ExceptionInfo? Exception { get; set; }
        public IDictionary<string, string> Properties { get; set; }
    }
}
