using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.Common.Exception
{
    public class DALException : System.Exception
    {
        public DALException(string message)
            : base(message)
        { }
    }
}
