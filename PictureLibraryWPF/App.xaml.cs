using Autofac;
using PictureLibraryViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PictureLibraryWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var container = ContainerConfig.Configure();

            var mainWindow = container.Resolve<MainWindow>();

            mainWindow.Show();
        }
    }
}
