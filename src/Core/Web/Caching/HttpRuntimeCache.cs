using System;
using System.Web.Caching;
using Dolstagis.Core.Caching;

namespace Dolstagis.Core.Web.Caching
{
    public class HttpRuntimeCache : ICache
    {
        private Cache _cache;

        public HttpRuntimeCache(Cache cache)
        {
            this._cache = cache;
        }

        public int Count { get { return _cache.Count; } }

        public object this[string key] {
            get { return _cache[key]; }
            set { _cache[key] = value; }
        }

        public object Add(string key, object value, CacheDependency dependencies,
            DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority,
            CacheItemRemovedCallback onRemoveCallback)
        {
            return _cache.Add(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public void Insert(string key, object value)
        {
            _cache.Insert(key, value);
        }

        public void Insert(string key, object value, CacheDependency dependencies)
        {
            _cache.Insert(key, value, dependencies);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            _cache.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration);
        }

        public void Insert(string key, object value, CacheDependency dependencies,
            DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            _cache.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, onUpdateCallback);
        }

        public void Insert(string key, object value, CacheDependency dependencies,
            DateTime absoluteExpiration, TimeSpan slidingExpiration,
            CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            _cache.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration,
                priority, onRemoveCallback);
        }

        public object Remove(string key)
        {
            return _cache.Remove(key);
        }
    }
}