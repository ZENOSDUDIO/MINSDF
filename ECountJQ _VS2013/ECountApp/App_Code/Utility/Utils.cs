using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using SGM.ECount.Contract.Service;
using System.Configuration;
using SGM.ECount.Service;
using System.ServiceModel;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

/// <summary>
/// Summary description for Utils
/// </summary>
public static class Utils
{
    private const string PROXY_CACHE_KEY = "_SvcProxy";
    /// <summary>
    /// add item into cache
    /// </summary>
    /// <param name="cacheKey">key of cache item</param>
    /// <param name="value">new item</param>
    public static void SetCache(string cacheKey, object value)
    {
        ICacheManager cacheMgr = CacheFactory.GetCacheManager();
        if (cacheMgr.Contains(cacheKey))
        {
            cacheMgr.Remove(cacheKey);
        }
        cacheMgr.Add(cacheKey, value);
    }


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

    public static IECountService GetCachedProxy()
    {
        string sessionID = HttpContext.Current.Session.SessionID;
        string cacheKey = sessionID + PROXY_CACHE_KEY;
        return GetCache(cacheKey) as IECountService;
    }

    public static void Logout()
    {
        CloseProxy();
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();
    }

    public static void CloseProxy()
    {
        bool inDMZ = false;
        if (bool.TryParse(ConfigurationManager.AppSettings["InDMZ"], out inDMZ) && inDMZ)
        {
            string sessionID = HttpContext.Current.Session.SessionID;
            string cacheKey = sessionID + PROXY_CACHE_KEY;
            ICacheManager cacheMgr = CacheFactory.GetCacheManager();
            if (cacheMgr.Contains(cacheKey))
            {
                cacheMgr.Remove(cacheKey);
            }
        }
    }

    /// <summary>
    /// add item into cache
    /// </summary>
    /// <param name="cacheKey">key of cache item</param>
    /// <param name="value">new item</param>
    public static void CacheProxy( object value, DateTime timeOut)
    {
        string cacheKey = HttpContext.Current.Session.SessionID + PROXY_CACHE_KEY;
        ICacheManager cacheMgr = CacheFactory.GetCacheManager();
        if (cacheMgr.Contains(cacheKey))
        {
            cacheMgr.Remove(cacheKey);
        }
        
        //HttpContext.Current.Cache.Add(cacheKey, value, null, DateTime.Now.AddMinutes(timeOut), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, new System.Web.Caching.CacheItemRemovedCallback(
        //    delegate
        //    {

        //    }
        //));
        cacheMgr.Add(cacheKey, value, CacheItemPriority.Normal, new ProxyCacheItemRefreshAction(),new AbsoluteTime(timeOut));
    }

    private class ProxyCacheItemRefreshAction:ICacheItemRefreshAction
    {
        #region ICacheItemRefreshAction Members

        public void Refresh(string removedKey, object expiredValue, CacheItemRemovedReason removalReason)
        {
            System.ServiceModel.ICommunicationObject obj = expiredValue as System.ServiceModel.ICommunicationObject;
            if (obj!=null)
            {
                try
                {
                    obj.Close();
                }
                catch (CommunicationException)
                {
                    obj.Abort();
                }
                catch (TimeoutException)
                {
                    obj.Abort();
                }
                catch (Exception)
                {
                    obj.Abort();
                    throw;
                }
            }
        }

        #endregion
    }

    public static IECountService GetService()
    {
        //IECountService service;
        //bool inDMZ = false;
        //service = HttpContext.Current.Session["ECountServiceProxy"] as IECountService;
        //if (service == null)
        //{
        //    if (bool.TryParse(ConfigurationManager.AppSettings["InDMZ"], out inDMZ) && inDMZ)
        //    {

        //        service = new ECountServiceProxy();
        //        HttpContext.Current.Session["ECountServiceProxy"] = service;

        //    }
        //    else
        //    {
        //        service = new Service();
        //    }
        //}
        //if (service is ECountServiceProxy)
        //{
        //    if (((ECountServiceProxy)service).State == System.ServiceModel.CommunicationState.Faulted)
        //    {
        //        service = new ECountServiceProxy();
        //        HttpContext.Current.Session["ECountServiceProxy"] = service;
        //    }
        //}
        //return service;

        IECountService service;
        bool inDMZ = false;
        //service = HttpContext.Current.Session["ECountServiceProxy"] as IECountService;
        //if (service == null)
        //{
            if (bool.TryParse(ConfigurationManager.AppSettings["InDMZ"], out inDMZ) && inDMZ)
            {
                service = GetCachedProxy();
                if (service == null || service != null && ((ICommunicationObject)service).State != System.ServiceModel.CommunicationState.Opened)
                {
                    service = new ECountServiceProxy();
                    ((ICommunicationObject)service).Open();
                    CacheProxy(service, DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout));
                }
                //service = HttpContext.Current.Session["ECountServiceProxy"] as IECountService;
                //if (service ==null || service !=null &&((ECountServiceProxy)service).State == System.ServiceModel.CommunicationState.Faulted)
                //{
                //    service = new ECountServiceProxy();
                //    HttpContext.Current.Session["ECountServiceProxy"] = service;
                //}

            }
            else
            {
                service = new Service();
            }
        ////}
        //if (service is ECountServiceProxy)
        //{
        //    if (((ECountServiceProxy)service).State == System.ServiceModel.CommunicationState.Faulted)
        //    {
            //service = ECountServiceProxy.Instance();
        //        //HttpContext.Current.Session["ECountServiceProxy"] = service;
        //    }
        //}
        return service;

    }
}
