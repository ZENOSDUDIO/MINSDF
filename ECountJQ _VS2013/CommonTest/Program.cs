using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.Common.Exception;
using System.Threading;
using SGM.Common.Log;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace CommonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ICacheManager cache = CacheFactory.GetCacheManager();

            cache.Add("test", "t");
            Console.WriteLine(cache.GetData("test"));

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            LogHelper.LogToDB("test log", "logging...");
            //try
            //{
            //    throw new System.Exception("test ex");
            //}
            //catch (Exception ex)
            //{
            //    ExceptionHandler.HandleDALException(ex);
            //}            
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHandler.HandleUIException((System.Exception)e.ExceptionObject);
            //Console.Write(e.ExceptionObject.ToString());
            
        }
    }
}
