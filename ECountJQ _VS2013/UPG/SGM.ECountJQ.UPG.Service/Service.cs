using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECountJQ.UPG.ServiceContract;
using System.ServiceModel;

namespace SGM.ECountJQ.UPG.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service : IService
    {
        public string HelloWorld()
        {
            SGM.ECountJQ.UPG.BLL.DiffAnalyse hw = new BLL.DiffAnalyse();
            return hw.HelloWorld();
        }
    }
}
