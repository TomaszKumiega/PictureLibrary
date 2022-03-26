using Autofac;
using PictureLibraryViewModel;
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
