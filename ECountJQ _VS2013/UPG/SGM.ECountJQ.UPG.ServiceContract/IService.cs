using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ServiceModel;
using SGM.Common.Exception;

namespace SGM.ECountJQ.UPG.ServiceContract
{
    public interface IService
    {
        [OperationContract, FaultContract(typeof(ServiceFault))]
        string HelloWorld();
    }
}
