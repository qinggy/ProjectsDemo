using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace Esd.EnergyPec.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            using (SettingHelper setting = new SettingHelper())
            {
                serviceInstaller1.ServiceName = setting.ServiceName;
                serviceInstaller1.DisplayName = setting.DisplayName;
                serviceInstaller1.Description = setting.Description;
                serviceInstaller1.ServicesDependedOn = new[] { setting.ServicesDependedOn };
            }
        }
    }
}
