using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace eCommerce.API.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExeptionHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionHandlingMiddleWare> _logger;

        public ExeptionHandlingMiddleWare(RequestDelegate next , ILogger<ExeptionHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex)
            {
                //log exeption type and Message
                _logger.LogError($"{ex.GetType().ToString()}:{ex.Message}");
                if(ex.InnerException is not null)
                {
                    //Log the inner exeption type and Message
                    _logger.LogError($"{ex.InnerException.GetType().ToString()}:{ex.InnerException.Message}");
                }

                httpContext.Response.StatusCode = 500;
                //Internal Server error 
                await httpContext.Response.WriteAsJsonAsync(new { Message = ex.Message, type = ex.GetType().ToString() });
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExeptionHandlingMiddleWareExtensions
    {
        public static IApplicationBuilder UseExeptionHandlingMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExeptionHandlingMiddleWare>();
        }
    }
}
