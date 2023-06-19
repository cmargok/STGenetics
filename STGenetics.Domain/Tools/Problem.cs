namespace STGenetics.Domain.Tools
{
    public class Problem : TailMessage
    {
        public int StatusCode { get; set; } = 500;
        public string Source { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
        public string TraceId { get; set; } = String.Empty;
    }
}
