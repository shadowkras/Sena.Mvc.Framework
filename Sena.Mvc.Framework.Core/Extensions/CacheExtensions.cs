using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Sena.Mvc.Framework.Core.Extensions
{
    /// <summary>
    /// Extension methods to work with Microsoft.Extensions.Caching.
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// Saves a memory cache response and returns the result as a string.
        /// </summary>
        /// <typeparam name="TEntity">Type of source entity.</typeparam>
        /// <param name="cache">IMemoryCache instance.</param>
        /// <param name="key">Cache key (needs to be unique).</param>
        /// <param name="method">Method that will be executed to get the data we want..</param>
        /// <param name="condition">Condition expression for the method.</param>
        /// <returns></returns>
        public static async Task<string> ResponseCacheString<TEntity>(this IMemoryCache cache, string key,
                                                                      Func<Expression<Func<TEntity, bool>>, Task<string>> method,
                                                                      Expression<Func<TEntity, bool>> condition = null)
        {
            if (cache.TryGetValue(key, out string cacheEntry) == false)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));

                var dados = await method(condition);
                cacheEntry = dados;

                cache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        /// <summary>
        /// Saves a memory cache response and returns the result as an object.
        /// </summary>
        /// <typeparam name="TEntity">Type of source entity.</typeparam>
        /// <param name="cache">IMemoryCache instance.</param>
        /// <param name="key">Cache key (needs to be unique).</param>
        /// <param name="method">Method that will be executed to get the data we want..</param>
        /// <param name="condition">Condition expression for the method.</param>
        public static async Task<object> ResponseCacheObject<TEntity>(this IMemoryCache cache, string key,
                                                                      Func<Expression<Func<TEntity, bool>>, Task<object>> method,
                                                                      Expression<Func<TEntity, bool>> condition = null)
        {
            if (cache.TryGetValue(key, out object cacheEntry) == false)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));

                var dados = await method(condition);
                cacheEntry = dados;

                cache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        /// <summary>
        /// Obtains an object from the application memory cache.
        /// </summary>
        /// <param name="cache">IMemoryCache instance.</param>
        /// <param name="key">Cache key (needs to be unique).</param>
        /// <returns></returns>
        public static object GetCache(this IMemoryCache cache, string key)
        {
            cache.TryGetValue(key, out object cacheEntry);
            return cacheEntry;
        }

        /// <summary>
        /// Saves an object in the application memory cache.
        /// </summary>
        /// <param name="cache">IMemoryCache instance.</param>
        /// <param name="key">Cache key (needs to be unique).</param>
        /// <param name="dataObject">Object with data to be saved.</param>
        /// <param name="duration">Cache duration, in seconds.</param>
        public static void SaveCache(this IMemoryCache cache, string key, object dataObject, int duration = 60)
        {
            if (cache.TryGetValue(key, out object cacheEntry) == false)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(duration));
                cache.Set(key, dataObject, cacheEntryOptions);
            }
        }
    }
}
