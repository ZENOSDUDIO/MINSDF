using System;

namespace ECount.Infrustructure
{
    public interface ILogger
    {
        ILogger Error(object sender, string msg);
        ILogger Error(object sender, Exception ex);
        ILogger Error(object sender, string msg, Exception ex);
        ILogger Info(object sender, string msg);
        ILogger Trace(object sender, string msg);
        ILogger Log(object sender, string msg);
    }
}