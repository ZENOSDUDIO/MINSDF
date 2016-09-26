using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace SGM.Common.Log
{
    public static class LogHelper
    {
        //public static void LogException(string message,Exception ex)
        //{
        //    LogException(message, null, ex);
        //}

        //public static void LogException(Exception ex)
        //{
        //    LogException(ex.Message, null, ex);
        //}

        //public static void LogException(IDictionary<string, object> extendedProperties, Exception ex)
        //{
        //    LogException(ex.Message, extendedProperties, ex);
        //}

        //public static void LogException(string message,IDictionary<string, object> extendedProperties, Exception ex)
        //{
        //    LogEntry entry = new LogEntry();
        //    entry.Title = message;
        //    entry.Message = ex.StackTrace;
        //    entry.ExtendedProperties = extendedProperties;
        //}

        public static void LogEvent(string category, string title, string message)
        {
            LogEvent(category,title, message, null);
        }

        public static void LogEvent(string category,string title, string message, IDictionary<string, object> extendedProperties)
        {
            LogEntry entry = new LogEntry();
            entry.Title = title;
            entry.Message = message;
            entry.Categories.Add(category);
            Logger.Write(entry);            
        }

        public static void LogToFile(string title,string message,  IDictionary<string, object> extendedProperties)
        {
            LogEvent(LogCategory.LOG_TO_FILE,title, message, extendedProperties);
        }

        public static void LogToFile(string title, string message)
        {
            LogEvent(LogCategory.LOG_TO_FILE, title, message);
        }

        public static void LogToDB(string title, string message)
        {
            LogEvent(LogCategory.LOG_TO_DB, title, message);
        }

        public static void LogToDB(string title,string message,  IDictionary<string, object> extendedProperties)
        {
            LogEvent(LogCategory.LOG_TO_DB, title, message, extendedProperties);
        }

        public static void LogToEventLog(string title, string message)
        {
            LogEvent(LogCategory.LOG_TO_EVENTLOG, title, message);
        }

        public static void LogToEventLog( string title, string message,IDictionary<string, object> extendedProperties)
        {
            LogEvent(LogCategory.LOG_TO_EVENTLOG, title, message, extendedProperties);
        }
    }

    internal struct LogCategory
    {
        public const string LOG_TO_FILE = "LogFile";
        public const string LOG_TO_DB = "LogDB";
        public const string LOG_TO_EVENTLOG = "LogEvent";
    }
}
