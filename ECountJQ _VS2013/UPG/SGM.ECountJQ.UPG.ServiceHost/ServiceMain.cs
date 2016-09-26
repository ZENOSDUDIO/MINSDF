using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using XServiceHost = System.ServiceModel.ServiceHost;

namespace SGM.ECountJQ.UPG.ServiceHost
{
    public partial class ServiceMain : ServiceBase
    {
        XServiceHost host;

        public ServiceMain()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (host != null)
            {
                host.Close();
                host = null;
            }
            host = new XServiceHost(typeof(SGM.ECountJQ.UPG.Service.Service));
            host.Open();
        }

        protected override void OnStop()
        {
            if (host != null)
            {
                host.Close();
                host = null;
            }
        }
    }
}
