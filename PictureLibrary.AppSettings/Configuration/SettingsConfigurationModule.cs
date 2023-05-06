using Autofac;

namespace PictureLibrary.AppSettings.Configuration
{
    public class SettingsConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly).AsImplementedInterfaces().SingleInstance();
        }
    }
}
