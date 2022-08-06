using Autofac;
using PictureLibraryModel.DataProviders;
using PictureLibraryModel.DataProviders.Builders;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Model.Settings;
using PictureLibraryModel.Services.CredentialsProvider;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.LibraryFileService;
using PictureLibraryModel.Services.RemoteStorageInfoSerializer;
using PictureLibraryModel.Services.SettingsProvider;
using PictureLibraryModel.Services.StringEncryption;
using System;

namespace PictureLibraryModel
{
    public class DataModelDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterModel(builder);
            RegisterServices(builder);
            RegisterDataAccess(builder);

            builder.RegisterGeneric(typeof(ImplementationSelector<,>)).As(typeof(IImplementationSelector<,>));

        }

        private void RegisterModel(ContainerBuilder builder)
        {
            builder.RegisterType<Drive>().AsSelf();
            builder.Register<Func<Drive>>(context =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<Drive>(); };
            });

            builder.RegisterType<LocalImageFile>().AsSelf();
            builder.Register<Func<LocalImageFile>>(context =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<LocalImageFile>(); };
            });

            builder.RegisterType<Folder>().AsSelf();
            builder.Register<Func<Folder>>(context =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<Folder>(); };
            });

            builder.RegisterType<LocalLibrary>().AsSelf();
            builder.Register<Func<LocalLibrary>>(context =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<LocalLibrary>(); };
            });

            builder.RegisterType<Tag>().AsSelf();
            builder.Register<Func<Tag>>(context =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<Tag>(); };
            });

            builder.RegisterType<Settings>().AsSelf();
            builder.Register<Func<Settings>>(context =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<Settings>(); };
            });

            builder.RegisterType<SerializableSettings>().AsSelf();
            builder.Register<Func<SerializableSettings>>(context =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<SerializableSettings>(); };
            });

            builder.RegisterType<LocalLibraryBuilder>().Keyed<ILibraryBuilder>(DataSourceType.Local);

            RegisterRemoteStorageInfos(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<LibraryFileService>().As<ILibraryFileService>();
            builder.RegisterType<SettingsProvider>().As<ISettingsProvider>().SingleInstance();
            builder.RegisterType<StringEncryptionService>().As<IStringEncryptionService>();
            builder.RegisterType<DirectoryService>().As<IDirectoryService>();
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<RemoteStorageInfosSerializer>().As<IRemoteStorageInfosSerializer>();
            builder.RegisterType<CredentialsProvider>().As<ICredentialsProvider>();
        }

        private void RegisterDataAccess(ContainerBuilder builder)
        {
            builder.RegisterType<LocalDataSourceBuilder>().Keyed<IDataSourceBuilder>(DataSourceType.Local);
            builder.RegisterType<DataSourceCollection>().As<IDataSourceCollection>();

            builder.RegisterType<DataSource>().As<IDataSource>();
            builder.Register<Func<IDataSource>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<IDataSource>(); };
            });

            builder.RegisterType<LocalImageFileProvider>().AsSelf();
            builder.Register<Func<LocalImageFileProvider>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<LocalImageFileProvider>(); };
            });

            builder.RegisterType<LocalLibraryProvider>().AsSelf();
            builder.Register<Func<LocalLibraryProvider>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<LocalLibraryProvider>(); };
            });

            builder.RegisterType<DataSourceCreator>().As<IDataSourceCreator>();
        }

        private void RegisterRemoteStorageInfos(ContainerBuilder builder)
        {
            builder.RegisterType<SerializableRemoteStorageInfo>().AsSelf();
            builder.Register<Func<SerializableRemoteStorageInfo>>(context =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<SerializableRemoteStorageInfo>(); };
            });

            builder.RegisterType<GoogleDriveRemoteStorageInfo>().As<IRemoteStorageInfo>().Keyed<DataSourceType>(DataSourceType.GoogleDrive);
        }
    }
}
