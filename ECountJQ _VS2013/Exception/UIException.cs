using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.Common.Exception
{
    public class UIException : BaseException
    {
        public UIException(string message)
            : base(message)
        { }
    }
}
