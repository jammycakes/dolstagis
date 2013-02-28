using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Core.Caching
{
    public class NullCache : ICache
    {
        public object Add(string key, object value, System.Web.Caching.CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, System.Web.Caching.CacheItemPriority priority, System.Web.Caching.CacheItemRemovedCallback onRemoveCallback)
        {
            return null;
        }

        public int Count
        {
            get { return 0; }
        }

        public object Get(string key)
        {
            return null;
        }

        public void Insert(string key, object value)
        { }

        public void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies)
        { }

        public void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        { }

        public void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, System.Web.Caching.CacheItemPriority priority, System.Web.Caching.CacheItemRemovedCallback onRemoveCallback)
        { }

        public void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, System.Web.Caching.CacheItemUpdateCallback onUpdateCallback)
        { }

        public object Remove(string key)
        {
            return null;
        }

        public object this[string key]
        {
            get
            {
                return null;
            }
            set
            { }
        }
    }
}
