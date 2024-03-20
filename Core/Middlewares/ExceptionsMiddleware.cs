using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate nextHandler, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _nextHandler = nextHandler ?? throw new ArgumentNullException(nameof(nextHandler));
        private readonly ILogger<ExceptionMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // https://stackoverflow.com/questions/59294099/net-core-return-iactionresult-from-a-custom-exception-middleware
        public async Task Invoke(HttpContext iContext)
        {
            try
            {
                await _nextHandler.Invoke(iContext);
            }
            catch (System.Exception iException)
            {
                iContext.Response.ContentType = "text/plain";
                iContext.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;

                await iContext.Response.WriteAsync(iException.Message);
                _logger.LogError($"An exception was caught when calling {iContext.Request.Path}...", iException);
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder iBuilder)
        {
            return iBuilder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
