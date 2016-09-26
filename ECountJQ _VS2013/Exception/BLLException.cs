using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.Common.Exception
{
    public class BLLException:System.Exception
    {
        public BLLException(string message)
            : base(message)
        { }
        public BLLException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
