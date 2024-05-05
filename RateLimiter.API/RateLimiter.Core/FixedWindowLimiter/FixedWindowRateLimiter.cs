namespace RateLimiter.Core.FixedWindowLimiter;

internal class FixedWindowRateLimiter : IRateLimiter
{
    private readonly Multiton<string, FixedWindow> _fixedWindows = new();

    public bool AllowRequest(string ip)
    {
        var fixedWindow = _fixedWindows.GetInstance(ip, () => new FixedWindow());
        return fixedWindow.IsAllowed();
    }
}