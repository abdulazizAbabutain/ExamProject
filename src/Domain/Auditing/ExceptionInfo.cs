namespace Domain.Auditing
{
    public class ExceptionInfo
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
        public ExceptionInfo? InnerException { get; set; }

        public static ExceptionInfo FromException(Exception ex) => new ExceptionInfo
        {
            Type = ex.GetType().FullName!,
            Message = ex.Message,
            StackTrace = ex.StackTrace,
            InnerException = ex.InnerException != null ? FromException(ex.InnerException) : null
        };
    }
}
