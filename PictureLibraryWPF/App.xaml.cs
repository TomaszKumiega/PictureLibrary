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
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e); 
            var container = PictureLibraryViewModel.ContainerConfig.Configure();
            MainWindow.DataContext = container.Resolve<IMainWindowViewModel>();
        }
    }
}
