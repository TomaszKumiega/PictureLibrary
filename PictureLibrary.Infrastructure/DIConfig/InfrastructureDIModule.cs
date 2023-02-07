using Autofac;
using PictureLibraryModel.DI_Configuration;

namespace PictureLibrary.Infrastructure
{
    public class InfrastructureDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ImplementationSelector<,>)).As(typeof(IImplementationSelector<,>));
        }
    }
}
