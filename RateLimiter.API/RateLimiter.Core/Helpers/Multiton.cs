using System.Collections.Concurrent;

namespace RateLimiter.Core.Helpers;

internal class Multiton<TKey, TValue> 
    where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, Lazy<TValue>> _dic = new();

    public TValue GetInstance(TKey key, Func<TValue> valuefactory)
    {
        // publication thread safty >> all threads that calls .Value get the same instance
        // Execution thread safty >> using a locking mechanism, valueFactory delegate only called once.
        return _dic.GetOrAdd(key, new Lazy<TValue>(valuefactory, LazyThreadSafetyMode.ExecutionAndPublication))
                   .Value;
    }
}
