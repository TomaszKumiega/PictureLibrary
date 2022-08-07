using Autofac;
using PictureLibraryModel.DataProviders;
using PictureLibraryModel.DataProviders.Builders;
using PictureLibraryModel.DataProviders.ImageProvider;
using PictureLibraryModel.DataProviders.LibraryProvider;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders;
using PictureLibraryModel.Model.FileSystemModel;
using PictureLibraryModel.Model.LibraryModel;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryModel.Model.Settings;
using PictureLibraryModel.Services.CredentialsProvider;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.GoogleDriveAPIClient;
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

            builder.RegisterType<GoogleDriveImageFile>().AsSelf();
            builder.Register<Func<GoogleDriveImageFile>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<GoogleDriveImageFile>(); };
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

            builder.RegisterType<GoogleDriveLibrary>().AsSelf();
            builder.Register<Func<GoogleDriveLibrary>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<GoogleDriveLibrary>(); };
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
            builder.RegisterType<GoogleDriveLibraryBuilder>().Keyed<ILibraryBuilder>(DataSourceType.GoogleDrive);

            RegisterRemoteStorageInfos(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<LibraryFileService>().As<ILibraryFileService>().SingleInstance();
            builder.RegisterType<SettingsProvider>().As<ISettingsProvider>().SingleInstance().SingleInstance();
            builder.RegisterType<StringEncryptionService>().As<IStringEncryptionService>().SingleInstance();
            builder.RegisterType<DirectoryService>().As<IDirectoryService>().SingleInstance();
            builder.RegisterType<FileService>().As<IFileService>().SingleInstance();
            builder.RegisterType<RemoteStorageInfosSerializer>().As<IRemoteStorageInfosSerializer>().SingleInstance();
            builder.RegisterType<CredentialsProvider>().As<ICredentialsProvider>().SingleInstance();
            builder.RegisterType<GoogleDriveAPIClient>().As<IGoogleDriveAPIClient>().SingleInstance();
        }

        private void RegisterDataAccess(ContainerBuilder builder)
        {
            builder.RegisterType<LocalDataSourceBuilder>().Keyed<IDataSourceBuilder>(DataSourceType.Local);
            builder.RegisterType<GoogleDriveDataSourceBuilder>().Keyed<IDataSourceBuilder>(DataSourceType.GoogleDrive);

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

            builder.RegisterType<GoogleDriveImageFileProvider>().AsSelf();
            builder.Register<Func<GoogleDriveImageFileProvider>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<GoogleDriveImageFileProvider>(); };
            });

            builder.RegisterType<LocalLibraryProvider>().AsSelf();
            builder.Register<Func<LocalLibraryProvider>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<LocalLibraryProvider>(); };
            });

            builder.RegisterType<GoogleDriveLibraryProvider>().AsSelf();
            builder.Register<Func<GoogleDriveLibraryProvider>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<GoogleDriveLibraryProvider>(); };
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

            builder.RegisterType<GoogleDriveRemoteStorageInfo>().AsSelf();
            builder.Register<Func<GoogleDriveRemoteStorageInfo>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<GoogleDriveRemoteStorageInfo>(); };
            });

            builder.RegisterType<GoogleDriveRemoteStorageInfo>().Keyed<IRemoteStorageInfo>(DataSourceType.GoogleDrive);
        }
    }
}
