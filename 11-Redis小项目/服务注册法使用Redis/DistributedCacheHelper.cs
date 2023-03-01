using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace 服务注册法使用Redis
{
    public class DistributedCacheHelper : IDistributedCacheHelper
    {
        private readonly IDistributedCache distCache;

        public DistributedCacheHelper(IDistributedCache distCache)
        {
            this.distCache = distCache;
        }

        private static DistributedCacheEntryOptions CreateOptions(int baseExpireSeconds)
        {
            TimeSpan value = TimeSpan.FromSeconds(Random.Shared.NextInt64(baseExpireSeconds, baseExpireSeconds * 2));//Random.Shared.NextInt64：NextDouble没有采用两个参数的重载，Random.Shared 是.NET6新增的
            return new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = value
            };
        }

        public TResult? GetOrCreate<TResult>(string cacheKey, Func<DistributedCacheEntryOptions, TResult?> valueFactory, int expireSeconds = 60)
        {
            string? @string = distCache.GetString(cacheKey);
            if (string.IsNullOrEmpty(@string))
            {
                DistributedCacheEntryOptions distributedCacheEntryOptions = CreateOptions(expireSeconds);
                TResult? val = valueFactory(distributedCacheEntryOptions);
                DistributedCacheExtensions.SetString(value: JsonSerializer.Serialize(val, typeof(TResult)), cache: distCache, key: cacheKey, options: distributedCacheEntryOptions);
                return val;
            }

            distCache.Refresh(cacheKey);
            return JsonSerializer.Deserialize<TResult>(@string);
        }

        public async Task<TResult?> GetOrCreateAsync<TResult>(string cacheKey, Func<DistributedCacheEntryOptions, Task<TResult?>> valueFactory, int expireSeconds = 60)
        {
            string? jsonStr = await distCache.GetStringAsync(cacheKey);
            if (string.IsNullOrEmpty(jsonStr))
            {
                DistributedCacheEntryOptions options = CreateOptions(expireSeconds);
                TResult? result = await valueFactory(options);
                string value = JsonSerializer.Serialize(result, typeof(TResult));
                await distCache.SetStringAsync(cacheKey, value, options);
                return result;
            }

            await distCache.RefreshAsync(cacheKey);
            return JsonSerializer.Deserialize<TResult>(jsonStr);
        }

        public void Remove(string cacheKey)
        {
            distCache.Remove(cacheKey);
        }

        public Task RemoveAsync(string cacheKey)
        {
            return distCache.RemoveAsync(cacheKey);
        }
    }
}
