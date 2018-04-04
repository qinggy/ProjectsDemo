using Esd.EnergyPec.CommonImp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string file =@"F:\EnergyPEC\CS在线更新工具\Esd.EnergyPec.UpdaterOnline\Esd.EnergyPec.UpdaterOnline\bin\Debug\Esd.EnergyPec.UpdaterOnline.exe";
            string parameters = @"F:\EnergyPEC\标准版-CS\Development\Esd.EnergyPec.DesktopApplication\bin\Debug.zip|13579609|C:\Program Files (x86)\Test";
            Process.Start(file, parameters);

            var factory = ServiceFactory.CreateService<ICheckUpdateService>();
            var result = factory.IsExistsNewest("B1426C99-39A2-4B7B-AE4D-2832F6A24F11", "V1.0.0.0");
            factory.CloseService();
        }
    }
}
