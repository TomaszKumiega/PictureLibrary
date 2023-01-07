﻿using Autofac;
using PictureLibraryModel.DataProviders;
using PictureLibraryModel.DataProviders.Builders;
using PictureLibraryModel.DataProviders.ImageProvider;
using PictureLibraryModel.DataProviders.LibraryProvider;
using PictureLibraryModel.DataProviders.Queries;
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
            builder.RegisterAsSelfWithFuncFactory<Drive>();
            builder.RegisterAsSelfWithFuncFactory<LocalImageFile>();
            builder.RegisterAsSelfWithFuncFactory<GoogleDriveImageFile>();
            builder.RegisterAsSelfWithFuncFactory<Folder>();
            builder.RegisterAsSelfWithFuncFactory<LocalLibrary>();
            builder.RegisterAsSelfWithFuncFactory<GoogleDriveLibrary>();
            builder.RegisterAsSelfWithFuncFactory<Tag>();
            builder.RegisterAsSelfWithFuncFactory<Settings>();
            builder.RegisterAsSelfWithFuncFactory<SerializableSettings>();

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

            builder.RegisterType<DataSourceCollection>().As<IDataSourceCollection>().SingleInstance();
            builder.RegisterType<DataSourceCreator>().As<IDataSourceCreator>();

            builder.RegisterWithInterfaceAndFuncFactory<DataSource, IDataSource>();

            builder.RegisterAsSelfWithFuncFactory<LocalImageFileProvider>();
            builder.RegisterAsSelfWithFuncFactory<GoogleDriveImageFileProvider>();
            builder.RegisterAsSelfWithFuncFactory<LocalLibraryProvider>();
            builder.RegisterAsSelfWithFuncFactory<GoogleDriveLibraryProvider>();
        }

        private void RegisterRemoteStorageInfos(ContainerBuilder builder)
        {
            builder.RegisterAsSelfWithFuncFactory<SerializableRemoteStorageInfo>();
            builder.RegisterAsSelfWithFuncFactory<GoogleDriveRemoteStorageInfo>();

            builder.RegisterType<GoogleDriveRemoteStorageInfo>().Keyed<IRemoteStorageInfo>(DataSourceType.GoogleDrive);
        }
    }
}
