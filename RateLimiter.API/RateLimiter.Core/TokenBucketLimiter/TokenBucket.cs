using Timer = System.Threading.Timer;

namespace RateLimiter.Core.TokenBucketLimiter;

internal class TokenBucket : IDisposable
{
    private const int Capacity = 10;
    private const int TokenAddingRateInMS = 1000;

    private readonly object _lock = new();
    private readonly Timer _timer;
    private volatile int _count = 0;

    public TokenBucket()
    {
        _count = Capacity;
        _timer = new (AddingTokenCallback, this, TokenAddingRateInMS, TokenAddingRateInMS);
    }

    public bool AcquireToken()
    {
        lock (_lock)
        {
            if (_count > 0)
            {
                _count--;
                return true;
            }
            return false;
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private void AddingTokenCallback(object? state)
    {
        lock (_lock)
        {
            if (_count < Capacity)
            {
                _count++;
            }
        }
    }

}
