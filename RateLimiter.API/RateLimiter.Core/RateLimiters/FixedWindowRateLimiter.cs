namespace RateLimiter.Core.RateLimiters;

internal class FixedWindowRateLimiter : IRateLimiter
{
    private const int MaxRequestsInWindow = 60;
    private readonly TimeSpan _size = TimeSpan.FromSeconds(60);
    private readonly object _lock = new();

    private DateTime _timestamp;
    private volatile int _counter = 0;


    public bool AllowRequest()
    {
        var currentTimestamp = new DateTime(DateTime.UtcNow.Ticks / _size.Ticks * _size.Ticks);
        lock (_lock)
        {
            if (currentTimestamp != _timestamp)
            {
                _timestamp = currentTimestamp;
                _counter = 1;
                return true;
            }

            if (_counter >= MaxRequestsInWindow)
            {
                return false;
            }

            _counter++;
            return true;
        }

    }
}
