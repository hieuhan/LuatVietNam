using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace LawsVN.Library
{
    public class CacheHelpers
    {
        /// <summary>
        /// MinuteFactor
        /// </summary>
        private static readonly int MinuteFactor = 15;

        /// <summary>
        /// cacheField
        /// </summary>
        private static readonly System.Web.Caching.Cache CacheField;

        static CacheHelpers()
        {
            HttpContext context = HttpContext.Current;
            CacheField = context != null ? context.Cache : HttpRuntime.Cache;
        }

        /// <summary>
        /// Đối tượng CacheLock
        /// </summary>
        private Object CacheLock = new Object();

        /// <summary>
        /// Property xác định việc lấy dữ liệu đã hoàn thành hay chưa
        /// </summary>
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
        /// AllKey
        /// </summary>
        public static List<string> AllKey
        {
            get
            {
                List<string> keyList = new List<string>();
                IDictionaryEnumerator cacheEnum = CacheField.GetEnumerator();
                while (cacheEnum.MoveNext())
                {
                    keyList.Add(cacheEnum.Key.ToString());
                }

                return keyList;
            }
        }

        /// <summary>
        /// Xoa cache theo key
        /// </summary>
        /// <param name="cacheKey">cacheKey</param>
        public static void Remove(string cacheKey)
        {
            CacheField.Remove(cacheKey);
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
        /// <param name="minutes">minutes - thoi gian tinh bang phut</param>
        public static void Insert<T>(string cacheKey, T obj, int minutes)
        {
            Insert(cacheKey, obj, null, minutes);
        }

        /// <summary>
        /// Insert to cache
        /// </summary>
        /// <param name="key">key de lam khoa</param>
        /// <param name="obj">doi tuong obj</param>
        /// <param name="minutes">minutes - thoi gian tinh bang phut</param>
        /// <param name="priority">thu tu uu tien</param>
        public static void Insert<T>(string cacheKey, T obj, int minutes, CacheItemPriority priority)
        {
            Insert(cacheKey, obj, null, minutes, priority);
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

        /// <summary>
        /// MaxInsert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="obj"></param>
        public static void MaxInsert<T>(string cacheKey, T obj)
        {
            MaxInsert(cacheKey, obj, null);
        }

        /// <summary>
        /// MaxInsert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="obj"></param>
        /// <param name="dep"></param>
        public static void MaxInsert<T>(string cacheKey, T obj, CacheDependency dep)
        {
            if (obj != null)
            {
                CacheField.Insert(cacheKey, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
            }
        }

        public T GetOrSet<T>(string cacheKey, Func<T> getDataFromDbCallback) where T : class
        {
            T item = CacheHelpers.Get<T>(cacheKey);
            if (item == null)
            {
                lock (CacheLock)
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