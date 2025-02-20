using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;



namespace BATBank_Tracker_Service
{
    [RunInstaller(true)] 
    public partial class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();

            
            serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem 
            };

            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "TrackerService", 
                DisplayName = "BAT Bank API Service Tracker",
                StartType = ServiceStartMode.Manual 
            };
          
            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}


