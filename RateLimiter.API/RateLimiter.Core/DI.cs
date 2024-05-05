using RateLimiter.Core.Enums;
using RateLimiter.Core.RateLimiterManager;
using RateLimiter.Core.RateLimiters;
using RateLimiter.Core.TokenBucketLimiter;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DI
    {
        public static IServiceCollection AddRateLimiter(this IServiceCollection services, RateLimiterType rateLimiterType)
        {
            return services
                    .AddSingleton<IRateLimiterManager, RateLimiterManager>()
                    .AddTransient<TokenBucketRateLimiter>()
                    .AddTransient<FixedWindowRateLimiter>()
                    .AddTransient<IRateLimiter>(services =>
                       rateLimiterType switch
                        {
                            RateLimiterType.TokenBucket => services.GetRequiredService<TokenBucketRateLimiter>(),
                            RateLimiterType.FixedWindow => services.GetRequiredService<FixedWindowRateLimiter>(),
                            _ => throw new NotImplementedException(),
                        }
                    )

                    ;
        } 
    }
}
