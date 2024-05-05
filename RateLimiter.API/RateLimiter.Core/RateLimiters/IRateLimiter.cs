namespace RateLimiter.Core.RateLimiters;

internal interface IRateLimiter
{
    bool AllowRequest();
}
