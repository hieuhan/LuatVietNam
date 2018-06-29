using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace VanBanLuat.Librarys
{
    public class CacheHelpers
    {
        private static readonly int MinuteFactor = 15;

        private static readonly Cache CacheField;

        static CacheHelpers()
        {
            HttpContext context = HttpContext.Current;
            CacheField = context != null ? context.Cache : HttpRuntime.Cache;
        }

        private Object sync = new Object();

        private bool _isSussces = false;
        public bool IsSussces
        {
            get
            {
                return _isSussces;
            }
            set
            {
                _isSussces = value;
            }
        }


        /// <summary>
        /// Insert to cache
        /// </summary>
        /// <param name="key">key de lam khoa</param>
        /// <param name="obj">doi tuong obj</param>
        /// <param name="dep">doi tuong dep</param>
        public static void Insert<T>(string cacheKey, T obj, CacheDependency dep)
        {
            Insert(cacheKey, obj, dep, MinuteFactor);
        }

        /// <summary>
        /// Insert to cache
        /// </summary>
        /// <param name="key">key de lam khoa</param>
        /// <param name="obj">doi tuong obj</param>
        /// <param name="dep">doi tuong dep</param>
        /// <param name="minutes">minutes - thoi gian tinh bang phut</param>
        public static void Insert<T>(string cacheKey, T obj, CacheDependency dep, int minutes)
        {
            Insert(cacheKey, obj, dep, minutes, CacheItemPriority.Normal);
        }

        /// <summary>
        /// Insert to cache
        /// </summary>
        /// <param name="key">key de lam khoa</param>
        /// <param name="obj">doi tuong obj</param>
        /// <param name="dep">doi tuong dep</param>
        /// <param name="minutes">minutes - thoi gian tinh bang phut</param>
        /// <param name="priority">thu tu uu tien</param>
        public static void Insert<T>(string key, T obj, CacheDependency dep, int minutes, CacheItemPriority priority)
        {
            if (obj != null)
            {
                CacheField.Insert(key, obj, dep, DateTime.Now.AddSeconds(minutes), TimeSpan.Zero, priority, null);
            }
        }

        public static T Get<T>(string cacheKey) where T : class
        {
            T item = CacheField[cacheKey] as T;
            return item;
        }

        public T GetOrSet<T>(string cacheKey, Func<T> getDataFromDbCallback) where T : class
        {
            T item = CacheHelpers.Get<T>(cacheKey);
            if (item == null)
            {
                lock (sync)
                {
                    if (!IsSussces)
                    {
                        item = getDataFromDbCallback();
                        CacheHelpers.Insert<T>(cacheKey, item, null);
                        this.IsSussces = true;
                    }
                }
            }
            return item;
        }
    }
}