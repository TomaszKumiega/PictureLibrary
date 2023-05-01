using Autofac;

namespace PictureLibrary.Settings.Configuration
{
    public class SettingsConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly).AsImplementedInterfaces().SingleInstance();
        }
    }
}
