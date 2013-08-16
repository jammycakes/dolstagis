using System;

namespace Dolstagis.Core.Caching
{
    /* ====== ICache interface ====== */

    /// <summary>
    ///  Provides an abstraction layer around the HTTP cache, or whatever other caching
    ///  mechanism we decide to replace it with.
    /// </summary>

    public interface ICache
    {
        /// <summary>
        ///  Adds the specified item to the System.Web.Caching.Cache object with dependencies,
        ///  expiration and priority policies, and a delegate you can use to notify your
        ///  application when the inserted item is removed from the Cache.
        /// </summary>
        /// <param name="key">
        ///  The cache key used to reference the item.
        /// </param>
        /// <param name="value">
        ///  The item to be added to the cache.
        /// </param>
        /// <param name="dependencies">
        ///  The file or cache key dependencies for the item. When any dependency changes,
        ///  the object becomes invalid and is removed from the cache. If there are no
        ///  dependencies, this parameter contains null.
        /// </param>
        /// <param name="absoluteExpiration">
        ///  The time at which the added object expires and is removed from the cache.
        ///  If you are using sliding expiration, the absoluteExpiration parameter must
        ///  be System.Web.Caching.Cache.NoAbsoluteExpiration.
        /// </param>
        /// <param name="slidingExpiration">
        ///  The interval between the time the added object was last accessed and the
        ///  time at which that object expires. If this value is the equivalent of 20
        ///  minutes, the object expires and is removed from the cache 20 minutes after
        ///  it is last accessed. If you are using absolute expiration, the slidingExpiration
        ///  parameter must be System.Web.Caching.Cache.NoSlidingExpiration.
        /// </param>
        /// <param name="priority">
        ///  The relative cost of the object, as expressed by the System.Web.Caching.CacheItemPriority
        ///  enumeration. The cache uses this value when it evicts objects; objects with
        ///  a lower cost are removed from the cache before objects with a higher cost.
        /// </param>
        /// <param name="onRemoveCallback">
        /// A delegate that, if provided, is called when an object is removed from the
        /// cache. You can use this to notify applications when their objects are deleted
        /// from the cache.
        /// </param>
        /// <returns>
        ///  An object that represents the item that was added if the item was previously
        ///  stored in the cache; otherwise, null.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///  The key or value parameter is set to null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///  The slidingExpiration parameter is set to less than TimeSpan.Zero or more
        ///  than one year.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///  The absoluteExpiration and slidingExpiration parameters are both set for
        ///  the item you are trying to add to the Cache.
        /// </exception>

        object Add(string key, object value, System.Web.Caching.CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, System.Web.Caching.CacheItemPriority priority, System.Web.Caching.CacheItemRemovedCallback onRemoveCallback);

        /// <summary>
        ///  Gets the number of items stored in the cache.
        /// </summary>

        int Count { get; }

        /// <summary>
        ///  Retrieves the specified item from the System.Web.Caching.Cache object.
        /// </summary>
        /// <param name="key">
        ///  The identifier for the cache item to retrieve.
        /// </param>
        /// <returns>
        ///  The retrieved cache item, or null if the key is not found.
        /// </returns>

        object Get(string key);


        /// <summary>
        ///  Inserts an item into the System.Web.Caching.Cache object with a cache key
        ///  to reference its location, using default values provided by the System.Web.Caching.CacheItemPriority
        ///  enumeration.
        /// </summary>
        /// <param name="key">
        ///  The cache key used to reference the item.
        /// </param>
        /// <param name="value">
        ///  The object to be inserted into the cache.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///  The key or value parameter is null.
        /// </exception>

        void Insert(string key, object value);

        /// <summary>
        ///  Inserts an item into the System.Web.Caching.Cache object with a cache key
        ///  to reference its location, using default values provided by the System.Web.Caching.CacheItemPriority
        ///  enumeration.
        /// </summary>
        /// <param name="key">
        ///  The cache key used to reference the item.
        /// </param>
        /// <param name="value">
        ///  The object to be inserted into the cache.
        /// </param>
        /// <param name="dependencies">
        ///  The file or cache key dependencies for the inserted object. When any dependency
        ///  changes, the object becomes invalid and is removed from the cache. If there
        ///  are no dependencies, this parameter contains null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///  The key or value parameter is null.
        /// </exception>

        void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies);


        /// <summary>
        ///  Inserts an object into the System.Web.Caching.Cache with dependencies and
        ///  expiration policies.
        /// </summary>
        /// <param name="key">
        ///  The cache key used to reference the item.
        /// </param>
        /// <param name="value">
        ///  The object to be inserted into the cache.
        /// </param>
        /// <param name="dependencies">
        ///  The file or cache key dependencies for the inserted object. When any dependency
        ///  changes, the object becomes invalid and is removed from the cache. If there
        ///  are no dependencies, this parameter contains null.
        /// </param>
        /// <param name="absoluteExpiration">
        ///  The time at which the inserted object expires and is removed from the cache.
        ///  To avoid possible issues with local time such as changes from standard time
        ///  to daylight saving time, use System.DateTime.UtcNow rather than System.DateTime.Now
        ///  for this parameter value. If you are using absolute expiration, the slidingExpiration
        ///  parameter must be System.Web.Caching.Cache.NoSlidingExpiration.
        /// </param>
        /// <param name="slidingExpiration">
        ///  The interval between the time the inserted object is last accessed and the
        ///  time at which that object expires. If this value is the equivalent of 20
        ///  minutes, the object will expire and be removed from the cache 20 minutes
        ///  after it was last accessed. If you are using sliding expiration, the absoluteExpiration
        ///  parameter must be System.Web.Caching.Cache.NoAbsoluteExpiration.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///  The key or value parameter is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///  The slidingExpiration parameter is set to less than TimeSpan.Zero or more
        ///  than one year.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///  The absoluteExpiration and slidingExpiration parameters are both set for
        ///  the item you are trying to add to the Cache.
        /// </exception>

        void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies,
            DateTime absoluteExpiration, TimeSpan slidingExpiration);

        /// <summary>
        ///  Inserts an object into the System.Web.Caching.Cache object with dependencies,
        ///  expiration and priority policies, and a delegate you can use to notify your
        ///  application when the inserted item is removed from the Cache.
        /// </summary>
        /// <param name="key">
        ///  The cache key used to reference the item.
        /// </param>
        /// <param name="value">
        ///  The object to be inserted into the cache.
        /// </param>
        /// <param name="dependencies">
        ///  The file or cache key dependencies for the inserted object. When any dependency
        ///  changes, the object becomes invalid and is removed from the cache. If there
        ///  are no dependencies, this parameter contains null.
        /// </param>
        /// <param name="absoluteExpiration">
        ///  The time at which the inserted object expires and is removed from the cache.
        ///  To avoid possible issues with local time such as changes from standard time
        ///  to daylight saving time, use System.DateTime.UtcNow rather than System.DateTime.Now
        ///  for this parameter value. If you are using absolute expiration, the slidingExpiration
        ///  parameter must be System.Web.Caching.Cache.NoSlidingExpiration.
        /// </param>
        /// <param name="slidingExpiration">
        ///  The interval between the time the inserted object is last accessed and the
        ///  time at which that object expires. If this value is the equivalent of 20
        ///  minutes, the object will expire and be removed from the cache 20 minutes
        ///  after it was last accessed. If you are using sliding expiration, the absoluteExpiration
        ///  parameter must be System.Web.Caching.Cache.NoAbsoluteExpiration.
        /// </param>
        /// <param name="priority">
        ///  The cost of the object relative to other items stored in the cache, as expressed
        ///  by the System.Web.Caching.CacheItemPriority enumeration. This value is used
        ///  by the cache when it evicts objects; objects with a lower cost are removed
        ///  from the cache before objects with a higher cost.
        /// </param>
        /// <param name="onRemoveCallback">
        ///  A delegate that, if provided, will be called when an object is removed from
        ///  the cache. You can use this to notify applications when their objects are
        ///  deleted from the cache. 
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///  The key or value parameter is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///  The slidingExpiration parameter is set to less than TimeSpan.Zero or more
        ///  than one year.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///  The absoluteExpiration and slidingExpiration parameters are both set for
        ///  the item you are trying to add to the Cache.
        /// </exception>

        void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies,
            DateTime absoluteExpiration, TimeSpan slidingExpiration,
            System.Web.Caching.CacheItemPriority priority,
            System.Web.Caching.CacheItemRemovedCallback onRemoveCallback);

        /// <summary>
        ///  Inserts an object into the System.Web.Caching.Cache object together with
        ///  dependencies, expiration policies, and a delegate that you can use to notify
        ///  the application before the item is removed from the cache.
        /// </summary>
        /// <param name="key">
        ///  The cache key used to reference the item.
        /// </param>
        /// <param name="value">
        ///  The object to be inserted into the cache.
        /// </param>
        /// <param name="dependencies">
        ///  The file or cache key dependencies for the inserted object. When any dependency
        ///  changes, the object becomes invalid and is removed from the cache. If there
        ///  are no dependencies, this parameter contains null.
        /// </param>
        /// <param name="absoluteExpiration">
        ///  The time at which the inserted object expires and is removed from the cache.
        ///  To avoid possible issues with local time such as changes from standard time
        ///  to daylight saving time, use System.DateTime.UtcNow rather than System.DateTime.Now
        ///  for this parameter value. If you are using absolute expiration, the slidingExpiration
        ///  parameter must be System.Web.Caching.Cache.NoSlidingExpiration.
        /// </param>
        /// <param name="slidingExpiration">
        ///  The interval between the time the inserted object is last accessed and the
        ///  time at which that object expires. If this value is the equivalent of 20
        ///  minutes, the object will expire and be removed from the cache 20 minutes
        ///  after it was last accessed. If you are using sliding expiration, the absoluteExpiration
        ///  parameter must be System.Web.Caching.Cache.NoAbsoluteExpiration.
        /// </param>
        /// <param name="onUpdateCallback">
        ///  A delegate that will be called before the object is removed from the cache.
        ///  You can use this to update the cached item and ensure that it is not removed
        ///  from the cache.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///  The key or value parameter is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///  The slidingExpiration parameter is set to less than TimeSpan.Zero or more
        ///  than one year.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///  The absoluteExpiration and slidingExpiration parameters are both set for
        ///  the item you are trying to add to the Cache.
        /// </exception>

        void Insert(string key, object value, System.Web.Caching.CacheDependency dependencies,
            DateTime absoluteExpiration, TimeSpan slidingExpiration,
            System.Web.Caching.CacheItemUpdateCallback onUpdateCallback);

        /// <summary>
        ///  Removes the specified item from the application's System.Web.Caching.Cache
        ///  object.
        /// </summary>
        /// <param name="key">
        ///  A System.String identifier for the cache item to remove.
        /// </param>
        /// <returns>
        ///  The item removed from the Cache. If the value in the key parameter is not
        ///  found, returns null.
        /// </returns>

        object Remove(string key);

        /// <summary>
        ///  Gets or sets the cache item at the specified key.
        /// </summary>
        /// <param name="key">
        ///  A System.String object that represents the key for the cache item.
        /// </param>
        /// <returns>
        ///  The specified cache item.
        /// </returns>

        object this[string key] { get; set; }
    }
}
