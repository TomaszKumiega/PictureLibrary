using Autofac;
using PictureLibraryModel.DataProviders;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model.Builders.ImageFileBuilder;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.SettingsProvider;
using PictureLibraryModel.Services.StringEncryption;
using System;

namespace PictureLibraryModel
{
    public class DataModelDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettingsProviderService>().As<ISettingsProviderService>().SingleInstance();
            builder.RegisterType<StringEncryptionService>().As<IStringEncryptionService>();
            builder.RegisterType<ImageFileBuilder>().As<IImageFileBuilder>();
            builder.RegisterType<DirectoryService>().As<IDirectoryService>();
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<LocalImageFileProvider>().AsSelf();
            builder.RegisterType<LocalLibraryProvider>().AsSelf();

            builder.Register<Func<LocalImageFileProvider>>((context) =>
            {
                var value = context.Resolve<LocalImageFileProvider>();
                return () => { return value; };
            });
            builder.Register<Func<LocalLibraryProvider>>((context) =>
            {
                var value = context.Resolve<LocalLibraryProvider>();
                return () => { return value; };
            });

            builder.RegisterGeneric(typeof(ImplementationSelector<,>)).As(typeof(IImplementationSelector<,>));

        }
    }
}
