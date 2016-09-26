using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGM.Common.Exception
{
    [DataContract]
    public class ServiceFault
    {
        [DataMember]
        public string Message { get; set; }
    }
}
