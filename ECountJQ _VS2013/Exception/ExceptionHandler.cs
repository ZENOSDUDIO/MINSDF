using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.IO;

namespace SGM.Common.Exception
{
    public static class ExceptionHandler
    {
        public static void HandleException(System.Exception ex, string exceptionType)
        {
            ExceptionHandlerFactory factory = new ExceptionHandlerFactory();
            ExceptionhandlerBase handler = factory.GetExceptionHandler(exceptionType);
            handler.HandleException(ex);
        }

        public static void HandleUIException(System.Exception ex)
        {
            ExceptionHandlerFactory factory = new ExceptionHandlerFactory();
            ExceptionhandlerBase handler = factory.GetExceptionHandler(ExceptionType.UI_EXCEPTION);
            handler.HandleException(ex);
            //ExceptionPolicy.HandleException(ex, ExceptionType.UI_EXCEPTION);
        }

        public static void HandleBLLException(System.Exception ex)
        {
            ExceptionHandlerFactory factory = new ExceptionHandlerFactory();
            ExceptionhandlerBase handler = factory.GetExceptionHandler(ExceptionType.BLL_EXCEPTION);
            handler.HandleException(ex);
        }

        public static void HandleDALException(System.Exception ex)
        {
            ExceptionHandlerFactory factory = new ExceptionHandlerFactory();
            ExceptionhandlerBase handler = factory.GetExceptionHandler(ExceptionType.DAL_EXCEPTION);
            handler.HandleException(ex);

        }

        public static System.Exception GetLastError()
        {
            if (System.Web.HttpContext.Current != null)
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                System.Exception lastError = context.Items["LastError"] as System.Exception;
                context.Server.ClearError();
                return lastError;
            }
            else
            {
                return null;
            }
        }
    }

    public abstract class ExceptionhandlerBase
    {
        protected virtual bool HandleException(System.Exception ex, string policy)
        {
            try
            {
                return ExceptionPolicy.HandleException(ex, policy);
            }
            catch (System.Exception innerEx)
            {
                LogException(innerEx);
                return true;
            }
        }

        private static void LogException(System.Exception innerEx)
        {
            using (StreamWriter writer = File.CreateText("ExceptionhandlerError.log"))
            {
                writer.Write("-------------------------------------------" + Environment.NewLine);
                writer.Write("time stamp:" + DateTime.Now.ToString() + Environment.NewLine);
                writer.Write(innerEx.ToString() + Environment.NewLine);
                writer.Close();
            }
        }

        protected virtual bool HandleException(System.Exception ex, string policy, out System.Exception outException)
        {
            outException = null;
            try
            {
                return ExceptionPolicy.HandleException(ex, policy, out outException);
            }
            catch (System.Exception innerEx)
            {
                LogException(innerEx);
                return true;
            }
        }

        public abstract void HandleException(System.Exception ex);
    }

    public class ExceptionHandlerFactory
    {
        private Dictionary<string, ExceptionhandlerBase> dictExceptionHandler;
        public ExceptionHandlerFactory()
        {
            dictExceptionHandler = new Dictionary<string, ExceptionhandlerBase>();
            dictExceptionHandler.Add(ExceptionType.BLL_EXCEPTION, new BLLExceptionHandler());
            dictExceptionHandler.Add(ExceptionType.DAL_EXCEPTION, new DALExceptionhandler());
            //dictExceptionHandler.Add(ExceptionType.SERVICE_EXCEPTION, new ServiceExceptionhandler());
            dictExceptionHandler.Add(ExceptionType.UI_EXCEPTION, new UIExceptionHandler());
        }
        public ExceptionhandlerBase GetExceptionHandler(string exceptionType)
        {
            return dictExceptionHandler[exceptionType];
        }
    }
    public class UIExceptionHandler : ExceptionhandlerBase
    {
        #region IExceptionHandler Members

        public override void HandleException(System.Exception ex)
        {
            base.HandleException(ex, ExceptionType.UI_EXCEPTION);
        }

        private System.Exception _lastError;
        public System.Exception LastError
        {
            get
            {
                if (System.Web.HttpContext.Current != null)
                {
                    _lastError = System.Web.HttpContext.Current.Items["LastError"] as System.Exception;
                }
                return _lastError;
            }
        }

        #endregion
    }

    public class BLLExceptionHandler : ExceptionhandlerBase
    {
        #region ExceptionhandlerBase Members

        public override void HandleException(System.Exception ex)
        {
            System.Exception newException;
            bool rethrow = base.HandleException(ex, ExceptionType.BLL_EXCEPTION, out newException);
            if (rethrow)
            {
                throw (newException != null) ? newException : ex;
            }
        }

        private System.Exception _lastError;
        public System.Exception LastError
        {
            get
            {
                return _lastError;
            }
        }
        #endregion
    }

    public class DALExceptionhandler : ExceptionhandlerBase
    {
        #region IExceptionHandler Members

        public override void HandleException(System.Exception ex)
        {
            System.Exception newException;
            bool rethrow = base.HandleException(ex, ExceptionType.DAL_EXCEPTION, out newException);
            if (rethrow)
            {
                if (newException != null)
                {
                    _lastError = newException;
                }
                else
                {
                    _lastError = ex;
                }
                throw (newException != null) ? newException : ex;
            }
        }

        private System.Exception _lastError;
        public System.Exception LastError
        {
            get
            {
                return _lastError;
            }
        }
        #endregion
    }

    public class GeneralExceptionHandler : ExceptionhandlerBase
    {
        public override void HandleException(System.Exception ex)
        {
            throw new NotImplementedException();
        }
    }
    
    public struct ExceptionType
    {

        public const string GENERAL_EXCEPTION = "General Exception Policy";
        public const string BLL_EXCEPTION = "BLL Exception Policy";
        public const string UI_EXCEPTION = "UI Exception Policy";
        public const string DAL_EXCEPTION = "DAL Exception Policy";
        public const string SERVICE_EXCEPTION = "Service Exception Policy";
        public const string SERVICE_PROXY_EXCEPTION = "Service Client Policy";
    }

    [Flags]
    public enum ExceptionLogMode
    {
        EventLog = 0x01,
        LogFile = 0x02,
        Database = 0x04
    }
}
