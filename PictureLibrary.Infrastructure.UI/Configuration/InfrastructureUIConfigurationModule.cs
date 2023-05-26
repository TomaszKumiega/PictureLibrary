using Autofac;

namespace PictureLibrary.Infrastructure.UI.Configuration
{
    public class InfrastructureUIConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PopupService>().As<IPopupService>().SingleInstance();
        }
    }
}
