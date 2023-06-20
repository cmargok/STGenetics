namespace STGenetics.API.Configurations.Middlewares
{

    /// <summary>
    /// 
    /// </summary>
    public class OperationCanceledMiddleware
    {
        private readonly RequestDelegate _next;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public OperationCanceledMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>      
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
                context.Response.StatusCode = 409;
            }
        }
    }
}
