using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace SGM.ECount.Service
{
    [RunInstaller(true)]
    public partial class ECountServiceInstaller : Installer
    {
        public ECountServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
