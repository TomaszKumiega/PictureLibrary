using Autofac;
using PictureLibrary.Libraries.UI.ViewModels;

namespace PictureLibrary.Libraries.UI.Configuration
{
    public class LibrariesUIConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LibrariesPageViewModel>().AsSelf().SingleInstance();
        }
    }
}
