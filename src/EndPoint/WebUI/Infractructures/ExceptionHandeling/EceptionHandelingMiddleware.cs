using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebUI.Infractructures
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class EceptionHandelingMiddleware
    {
        private readonly RequestDelegate _next;

        public EceptionHandelingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = ex.Message;
                await httpContext.Response.WriteAsync(ex.Message);
                return;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class EceptionHandelingMiddlewareExtensions
    {
        public static IApplicationBuilder UseEceptionHandelingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EceptionHandelingMiddleware>();
        }
    }
}
