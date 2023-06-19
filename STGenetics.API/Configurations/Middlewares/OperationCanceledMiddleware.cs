namespace STGenetics.API.Configurations.Middlewares
{

    /// <summary>
    /// 
    /// </summary>
    public class OperationCanceledMiddleware
    {
        private readonly RequestDelegate _next;
       // private readonly IApiLogger _logger;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public OperationCanceledMiddleware(RequestDelegate next/*, IApiLogger logger*/)
        {
            _next = next;
           // _logger = logger;
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OperationCanceledException)
            {
               // _logger.LoggingInformation("Request was cancelled - 409");
                context.Response.StatusCode = 409;
            }
        }
    }
}
