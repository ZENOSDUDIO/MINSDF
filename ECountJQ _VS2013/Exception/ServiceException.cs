using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.Common.Exception
{
    public class ServiceException : BaseException
    {
        public ServiceException(string message)
            : base(message)
        { }
    }
}
