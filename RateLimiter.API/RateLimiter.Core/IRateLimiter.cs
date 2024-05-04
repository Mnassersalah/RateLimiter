namespace RateLimiter.Core
{
    public interface IRateLimiter
    {
        bool AllowRequest(string ip);
    }
}
