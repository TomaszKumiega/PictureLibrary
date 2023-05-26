using Autofac;
using PictureLibrary.Infrastructure.Extensions;
using PictureLibraryModel.Builders;

namespace PictureLibraryModel.Configuration
{
    public class ModelConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterWithFuncFactory<LibraryBuilder, ILibraryBuilder>();
        }
    }
}
