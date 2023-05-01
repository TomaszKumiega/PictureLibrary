using Autofac;

namespace PictureLibrary.Tools.Configuration
{
    public class ToolsConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly).AsImplementedInterfaces().SingleInstance();
        }
    }
}
