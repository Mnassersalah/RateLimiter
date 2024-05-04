using RateLimiter.Core;
using RateLimiter.Core.TokenBucketLimiter;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DI
    {
        public static IServiceCollection AddRateLimiter(this IServiceCollection services)
        {
            return services
                    .AddSingleton<IRateLimiter, TokenBucketRateLimiter>()
                    ;
        } 
    }
}
