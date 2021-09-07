using Autofac;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders.ImageFileBuilder;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.ImageProviderService;
using PictureLibraryModel.Services.SettingsProvider;
using PictureLibraryModel.Services.StringEncryption;

namespace PictureLibraryModel
{
    public class DataModelDIModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettingsProviderService>().As<ISettingsProviderService>().SingleInstance();
            builder.RegisterType<StringEncryptionService>().As<IStringEncryptionService>();
            builder.RegisterType<ImageFileBuilder>().As<IImageFileBuilder>();
            builder.RegisterType<DirectoryService>().As<IDirectoryService>();
            builder.RegisterType<FileService>().As<IFileService>();

            builder.RegisterType<ImageProviderContext>().As<IImageProviderContext>();
            builder.RegisterType<ImageProviderStrategyFactory>().As<IImageProviderStrategyFactory>();
            builder.RegisterType<ImageProviderService>().As<IImageProviderService>();

        }
    }
}
