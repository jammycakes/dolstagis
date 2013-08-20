using System;

namespace Dolstagis.Framework.Caching
{
    public static class CacheExtensions
    {
        /// <summary>
        ///  Gets an item from the cache, or creates it if it doesn't exist.
        /// </summary>
        /// <typeparam name="T">
        ///  The type of the object to retrieve.
        /// </typeparam>
        /// <param name="cache">
        ///  The cache from which to retrieve the object.
        /// </param>
        /// <param name="key">
        ///  The key of the object to get.
        /// </param>
        /// <param name="factory">
        ///  A factory method to call if the object is not found, or is of an
        ///  incompatible type.
        /// </param>
        /// <returns>
        ///  The requested object from the cache.
        /// </returns>

        public static T Get<T>(this ICache cache, string key, Func<T> factory)
        {
            var result = cache.Get(key);
            if (!(result is T)) {
                result = factory();
                cache.Insert(key, result);
            }
            return (T)result;
        }
    }
}
