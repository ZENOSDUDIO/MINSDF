using System;
using System.Text;

namespace ECount.Infrustructure
{
    public static class Logger
    {
        /// <summary>
        ///  this instance is configed and activated by user.
        /// </summary>
        public static ILogger Instance
        {
            get
            {
                // TODO: currently just mock
                ILogger logger = new Log4NetLogger();
                // never return null to allow user always trust on this.
                return logger ?? Default;
            }
        }

        /// <summary>
        /// this instance is making sure the log never failed.
        /// </summary>
        public static ILogger Default
        {
            get
            {
                return new NullLogger();
            }
        }

        #region Ugly but ... just for migration now

        /// <summary>
        /// 记录消息日志
        /// 本方法为写信息日志推荐方法
        /// </summary>
        public static void LogInfo(string modelName, string functionName, string className, string msg)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append("\r\n--------------------------------------------------------------------------------------\r\n");
            logMessage.AppendFormat("{0} {1} {2} {3} {4} {5} ",
                DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss:fff"), modelName, className, functionName, 1, msg);

            Instance.Info(null, logMessage.ToString());
        }

        public static void LogInfo(string modelName, string functionName, int currentUserID, string msg, string className)
        {
            LogInfo(modelName, functionName, className, currentUserID + ";" + msg);
        }

        #region 为了兼容以前的写法而重载，以后不推荐这种写入log方式
        /// <summary>
        /// 为了兼容以前的写法而重载，以后不推荐这种写入log方式
        /// </summary>
        public static void LogError(string modelName, string fuctionName, AppError infoOrError, int errorCode, Exception e, string context, string externed)
        {
            string errorCodeStr = errorCode == 0 ? "E_99999" : errorCode.ToString();
            LogError(modelName, fuctionName, modelName, e.Message, errorCodeStr, e);
        }
        /// <summary>
        /// 为了兼容以前的写法而重载，以后不推荐这种写入log方式
        /// </summary>
        public static void LogError(string modelName, string fuctionName, AppError infoOrError, string errorCode, Exception e, string context, string externed)
        {

            LogError(modelName, fuctionName, modelName, e.Message, errorCode, e);
        }

        /// <summary>
        /// 为了兼容以前的写法而重载，以后不推荐这种写入log方式
        /// </summary>
        public static void LogError(string modelName, string fuctionName, AppError infoOrError, string errorCode, string context, string errorMessage, string externed)
        {

            LogError(modelName, fuctionName, modelName, errorMessage, errorCode, null);
        }
        /// <summary>
        /// 为了兼容以前的写法而重载，以后不推荐这种写入log方式
        /// </summary>
        public static void LogError(string modelName, string fuctionName, AppError infoOrError, int errorCode, string context, string errorMessage, string externed)
        {

            string errorCodeStr = errorCode == 0 ? "E_99999" : errorCode.ToString();
            LogError(modelName, fuctionName, modelName, errorMessage, errorCodeStr, null);
        }
        #endregion

        /// <summary>
        /// 写入日志重载方法
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="functionName"></param>
        /// <param name="className"></param>
        /// <param name="msg"></param>
        public static void LogError(string modelName, string functionName, string className, string msg)
        {
            LogError(modelName, functionName, className, msg, "", null);
        }

        public static void LogError(string modelName, string functionName, string className, string msg, Exception ex)
        {
            LogError(modelName, functionName, className, msg, "", ex);
        }
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="modelName">Assembly+Namespace</param>
        /// <param name="functionName">method name</param>
        /// <param name="className">class name</param>
        /// <param name="msg">错误消息内容</param>
        /// <param name="errorCode">错误码</param>
        public static void LogError(string modelName, string functionName, string className, string msg, string errorCode, Exception ex)
        {
            if (string.IsNullOrEmpty(errorCode)) errorCode = "E_99999";//内置默认错误码 
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append("\r\n--------------------------------------------------------------------------------------\r\n");
            string errorMsg = "";
            if (ex != null)
            {
                errorMsg = ex.ToString();
            }
            logMessage.AppendFormat("{0};{1};{2};{3};{4};{5};{6};{7}",
                DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss:fff"), modelName, functionName, className, 2, errorCode, msg, errorMsg);

            Instance.Error(null, logMessage.ToString());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logmsg"></param>
        /// <param name="loggerName"></param>
        public static void Log(string logmsg, string loggerName)
        {
            log4net.LogManager.GetLogger(loggerName).Info(logmsg);
        }

        #endregion
    }
}