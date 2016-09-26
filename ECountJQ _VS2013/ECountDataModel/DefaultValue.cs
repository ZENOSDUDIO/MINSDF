using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGM.ECount.DataModel
{
    public struct DefaultValue
    {
        public const int INT = default(int);
        public const long LONG = default(long);
        public const string GUID = "00000000-0000-0000-0000-000000000000";
        public const string STRING = default(string);
    }
}
