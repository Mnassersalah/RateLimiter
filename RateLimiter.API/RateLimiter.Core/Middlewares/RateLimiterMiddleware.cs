using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RateLimiter.Core;
using System.Net;

namespace RateLimiter.API.Middlewares
{
    public class RateLimiterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRateLimiter _rateLimiter;

        public RateLimiterMiddleware(RequestDelegate next, IRateLimiter rateLimiter)
        {
            _next = next;
            _rateLimiter = rateLimiter;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIP = context.Connection.RemoteIpAddress;

            if (clientIP == null || _rateLimiter.AllowRequest(clientIP.ToString()))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.Response.WriteAsync("Too Many Requests");
            }
        }
    }

    public static class SimpleRateLimiterMiddlewareExtensions
    {
        public static IApplicationBuilder UseSimpleRateLimiterMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RateLimiterMiddleware>();
        }
    }
}
