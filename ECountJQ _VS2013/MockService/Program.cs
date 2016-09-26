using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SGM.ECount.Service;
using SGM.ECount.Contract.Service;
using SGM.Common.Exception;
using SGM.ECount.DataModel;

namespace MockService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Service));
            host.Open();
            Console.WriteLine("ready...");

            ChannelFactory<IECountService> factory = new ChannelFactory<IECountService>("client");
            IECountService serviceProxy = factory.CreateChannel();
            try
            {
                serviceProxy.Login();
                //Part p =new Part{ Plant=new Plant{PlantID=5}};
                //int pageCount;
                //int itemcCount;
                //serviceProxy.QueryPartByPage(p, 100, 1, out pageCount,out itemcCount);
                Console.ReadLine();
            }
            catch (FaultException<ServiceFault> ex)
            {
                ExceptionHandler.HandleException(ex, ExceptionType.SERVICE_PROXY_EXCEPTION);
                Console.WriteLine(ex.Detail.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
