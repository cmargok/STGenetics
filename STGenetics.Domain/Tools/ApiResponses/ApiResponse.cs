namespace STGenetics.Domain.Tools.ApiResponses
{
    public class ApiResponse<T> : TailMessage
    {
        public T? Data { get; set; }

    }
}
