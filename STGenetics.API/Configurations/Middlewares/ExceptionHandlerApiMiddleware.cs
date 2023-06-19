using STGenetics.Domain.Tools;
using STGenetics.Domain.Tools.ApiResponses;
using System.Text.Json;

namespace STGenetics.API.Configurations.Middlewares
{
    /// <summary>
    /// Exception Handler middleware
    /// </summary>
    public class ExceptionHandlerApiMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly IApiLogger _logger;

        /// <summary>
        /// Exception Handler middleware Constructor
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerApiMiddleware(RequestDelegate next/*, IApiLogger logger*/)
        {
            _next = next;
           // _logger = logger;
        }

        /// <summary>
        /// Invoke next move
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
              //  _logger.LoggingError(ex, ex.Message);
               // _logger.LoggingInformation($"{ex.Source} - ERROR");
                await HandleExceptionAsync(context, ex);
            }
        }


        private static Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var problem = new Problem()
            {
                DataCount = 0,
                Result = Result.InternalServerError.GetDescription(),
                Source = error.Source!,
                StatusCode = 500,
                Message = error.Message,
                TraceId = context.TraceIdentifier
            };

            if (error.InnerException != null)
            {
                problem.Source = error.InnerException.Source!;
            }

            var json = JsonSerializer.Serialize(problem);
            return context.Response.WriteAsync(json);
        }

    }
}
