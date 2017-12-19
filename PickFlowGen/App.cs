using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickFlowGen
{
    class App
    {
        [STAThread]
        static void Main()
        {
            //Create an instance of a new Application
            System.Windows.Application _wpfApplication = new System.Windows.Application();

            //Set the start up URI as your XAML page 
            _wpfApplication.StartupUri = new Uri("MainWindow.xaml", UriKind.RelativeOrAbsolute);

            //Run this Application by callinf the Run() method
            _wpfApplication.Run();
        }

    }
}
