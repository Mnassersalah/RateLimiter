using RateLimiter.Core.Helpers;

namespace RateLimiter.Core.TokenBucketLimiter;

internal class TokenBucketRateLimiter : IRateLimiter
{
    private readonly Multiton<string, TokenBucket> _buckets = new();

    public bool AllowRequest(string ip)
    {
        var bucket = _buckets.GetInstance(ip, () => new TokenBucket());
        return bucket.AcquireToken();
    }
}
