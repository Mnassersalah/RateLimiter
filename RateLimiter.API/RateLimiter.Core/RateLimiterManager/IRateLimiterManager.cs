namespace RateLimiter.Core.RateLimiterManager;

internal interface IRateLimiterManager
{
    bool AllowIPRequest(string ip);
}
