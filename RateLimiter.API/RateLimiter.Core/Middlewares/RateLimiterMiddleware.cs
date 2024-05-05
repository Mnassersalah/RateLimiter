using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RateLimiter.Core.RateLimiterManager;
using System.Net;

namespace RateLimiter.API.Middlewares;

internal class RateLimiterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRateLimiterManager _rateLimiterManager;

    public RateLimiterMiddleware(RequestDelegate next, IRateLimiterManager rateLimiterManager)
    {
        _next = next;
        _rateLimiterManager = rateLimiterManager;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var clientIP = context.Connection.RemoteIpAddress;

        if (clientIP == null || _rateLimiterManager.AllowIPRequest(clientIP.ToString()))
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
