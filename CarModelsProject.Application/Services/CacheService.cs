using Microsoft.Extensions.Caching.Memory;

using System.Collections.Concurrent;

namespace CarModelsProject.Application.Services
{
    public class CacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, byte> _keys;
        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
            _keys = new ConcurrentDictionary<string, byte>();
        }
        public void Set<T>(string key, T value)
        {
            _cache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
                SlidingExpiration = TimeSpan.FromMinutes(1)
            });
            _keys.TryAdd(key, 0);
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            return _cache.TryGetValue(key, out value);
        }

        public void InvalidateCache(string prefixKey)
        {
            if (string.IsNullOrEmpty(prefixKey))
                return;

            var keysToRemove = _keys.Keys
                                        .Where(k => k.StartsWith(prefixKey, StringComparison.OrdinalIgnoreCase))
                                        .ToList();

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
                _keys.TryRemove(key, out _);
            }
        }

        public void ClearCache()
        {
            foreach (var key in _keys.Keys)
            {
                _cache.Remove(key);
            }
            _keys.Clear();
        }
    }
}
