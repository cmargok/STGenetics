namespace STGenetics.Domain.Tools
{
    public class ApiResponse<T> : TailMessage
    {
        public T? Data { get; set; }

    }
}
