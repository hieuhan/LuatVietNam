using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using ICSoft.CMSLib;

namespace ICSoft.HelperLib
{
    public class CacheHelpers<T>
        where T : class
    {
        /// <summary>
        /// Insert data to Cache
        /// </summary>        
        /// <param name="cacheName">
        /// The cache Name. 
        /// </param>
        /// <param name="l_Data">
        /// the data.
        /// </param>
        /// <returns>
        /// </returns>
        public static void InsertList(string cacheName, List<T> l_Data)
        {
            try
            {
            CacheDependency m_CacheDependency = null;
            DateTime ApsoluteExpiration = DateTime.UtcNow.AddMinutes(5.0);
            HttpRuntime.Cache.Add(
                cacheName,
                l_Data,
                m_CacheDependency,
                ApsoluteExpiration,
                TimeSpan.Zero,
                CacheItemPriority.Default,
                CacheRemovedCallback);
            }
            catch (Exception ex)
            {                
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        /// <summary>
        /// Insert data to Cache width cachekey is Full Name of T
        /// </summary>   
        /// <param name="l_Data">
        /// the data.
        /// </param>
        /// <returns>
        /// </returns>
        public static void InsertList( List<T> l_Data)
        {            
            try
            {
                string cacheName = "LIST-" + typeof(T).FullName;
                InsertList(cacheName, l_Data);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        /// <summary>
        /// Insert data to Cache
        /// </summary>        
        /// <param name="cacheName">
        /// The cache Name. 
        /// </param>
        /// <param name="l_Data">
        /// the data.
        /// </param>
        /// <returns>
        /// </returns>
        public static void Insert(string cacheName, T m_Data)
        {
            try
            {
                CacheDependency m_CacheDependency = null;
                DateTime ApsoluteExpiration = DateTime.UtcNow.AddMinutes(5.0);
                HttpRuntime.Cache.Add(
                    cacheName,
                    m_Data,
                    m_CacheDependency,
                    ApsoluteExpiration,
                    TimeSpan.Zero,
                    CacheItemPriority.Default,
                    CacheRemovedCallback);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        /// <summary>
        /// Insert data to Cache width cachekey is Full Name of T
        /// </summary>   
        /// <param name="l_Data">
        /// the data.
        /// </param>
        /// <returns>
        /// </returns>
        public static void Insert(T m_Data)
        {
            try
            {
                string cacheName = typeof(T).FullName;
                Insert(cacheName, m_Data);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        /// <summary>
        /// Get data from Cache
        /// </summary>
        /// <param name="cacheName">
        /// The Cache Name. 
        /// </param>
        /// <returns>
        /// List<T>
        /// </returns>
        public static List<T>  GetList(string cacheName)
        {
            List<T> RetVal = null;// new List<T>();
            try
            {
                RetVal = (List<T>)HttpRuntime.Cache.Get(cacheName);

            }
            catch(Exception ex)
            {
                RetVal = null;
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
        /// <summary>
        /// Get data from Cache width cachekey is Full Name of T
        /// </summary>        
        /// <returns>
        /// List<T>
        /// </returns>
        public static List<T> GetList()
        {
            try
            {
                string cacheName = "LIST-" + typeof(T).FullName;
                return GetList(cacheName);

            }
            catch (Exception ex)
            {                
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// Get data from Cache
        /// </summary>
        /// <param name="cacheName">
        /// The Cache Name. 
        /// </param>
        /// <returns>
        /// List<T>
        /// </returns>
        public static T Get(string cacheName)
        {
            T RetVal = null;// new List<T>();
            try
            {
                RetVal = (T)HttpRuntime.Cache.Get(cacheName);

            }
            catch (Exception ex)
            {
                RetVal = null;
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
        /// <summary>
        /// Get data from Cache width cachekey is Full Name of T
        /// </summary>        
        /// <returns>
        /// List<T>
        /// </returns>
        public static T Get()
        {
            try
            {
                string cacheName = typeof(T).FullName;
                return Get(cacheName);

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// log when cache removed
        /// </summary>      
        /// <returns>
        /// </returns>        
        public static void CacheRemovedCallback(String key, object value, System.Web.Caching.CacheItemRemovedReason removedReason)
        {
            //sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + 
            //    "The cached value with key '" + key +
            //"' was removed from the cache.  Reason: " +
            //removedReason.ToString());
        }

    }
}
