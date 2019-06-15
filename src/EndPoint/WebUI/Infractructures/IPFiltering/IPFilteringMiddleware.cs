using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WebUI.Contracts;

namespace WebUI.Infractructures
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class IPFilteringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IIPChecker _iPChecker;

        public IPFilteringMiddleware(RequestDelegate next,IIPChecker iPChecker)
        {
            _next = next;
            _iPChecker = iPChecker;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var remoteIp = httpContext.Connection.RemoteIpAddress;
            var localIp= httpContext.Connection.LocalIpAddress;

            if (_iPChecker.IsBlackIp(remoteIp))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
            else
            {
                await _next(httpContext);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class IPFilteringMiddlewareExtensions
    {
        public static IApplicationBuilder UseIPFilteringMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IPFilteringMiddleware>();
        }
    }
}
