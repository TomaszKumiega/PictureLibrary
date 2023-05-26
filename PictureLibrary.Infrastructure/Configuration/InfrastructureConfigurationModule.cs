using Autofac;
using PictureLibrary.Infrastructure.ImplementationSelector;

namespace PictureLibrary.Infrastructure.Configuration
{
    public class InfrastructureConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ImplementationSelector<,>)).As(typeof(IImplementationSelector<,>)).SingleInstance();
        }
    }
}
