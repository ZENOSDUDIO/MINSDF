using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace SGM.Common.Cache
{
    public static class CacheHelper
    {
        /// <summary>
        /// retrieve item from cache
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <returns>item founded,null if it's not in cache</returns>
        public static object GetCache(string cacheKey)
        {
            ICacheManager cacheMgr = CacheFactory.GetCacheManager();
            return cacheMgr.GetData(cacheKey);
        }
        /// <summary>
        /// add item into cache
        /// </summary>
        /// <param name="cacheKey">key of cache item</param>
        /// <param name="value">new item</param>
        public static void SetCache(string cacheKey, object value)
        {
            ICacheManager cacheMgr = CacheFactory.GetCacheManager();
            
            //if (cacheMgr.Contains(cacheKey))
            //{
            //    cacheMgr.Remove(cacheKey);
            //}
            cacheMgr.Add(cacheKey, value, CacheItemPriority.Normal,null, new SlidingTime(TimeSpan.FromMinutes(10)));
        }

        /// <summary>
        /// add item into cache
        /// </summary>
        /// <param name="cacheKey">key of cache item</param>
        /// <param name="value">new item</param>
        /// <param name="absoluteSpan">absolute timespan</param>
        public static void SetCache(string cacheKey, object value,TimeSpan absoluteSpan)
        {
            ICacheManager cacheMgr = CacheFactory.GetCacheManager();
            cacheMgr.Add(cacheKey, value, CacheItemPriority.Normal, null, new AbsoluteTime(absoluteSpan));
        }

        public static void RemoveCache(string cacheKey)
        {
            ICacheManager cacheMgr = CacheFactory.GetCacheManager();
            cacheMgr.Remove(cacheKey);
        }
    }


}
