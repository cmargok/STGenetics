namespace STGenetics.Domain.Tools.ApiResponses
{
    public class Problem : TailMessage
    {
        public int StatusCode { get; set; } = 500;
        public string Source { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string TraceId { get; set; } = string.Empty;
    }
}
