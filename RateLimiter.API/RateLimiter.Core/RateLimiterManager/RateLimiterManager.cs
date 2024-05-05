using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Core.RateLimiters;

namespace RateLimiter.Core.RateLimiterManager;

internal class RateLimiterManager : IRateLimiterManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Multiton<string, IRateLimiter> _rateLimiters = new();

    public RateLimiterManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public bool AllowIPRequest(string ip)
    {
        var rateLimiter = _rateLimiters.GetInstance(ip, () => _serviceProvider.GetRequiredService<IRateLimiter>());
        return rateLimiter.AllowRequest();
    }
}
