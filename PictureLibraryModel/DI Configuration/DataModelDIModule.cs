using Autofac;
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

namespace PictureLibraryModel
{
    public class DataModelDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterModel(builder);
            RegisterServices(builder);
            RegisterDataAccess(builder);

            

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
            builder.RegisterAsSelfWithFuncFactory<StorageQuery>();

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
            builder.RegisterType<GoogleDriveApiClient>().As<IGoogleDriveApiClient>().SingleInstance();
        }

        private void RegisterDataAccess(ContainerBuilder builder)
        {
            builder.RegisterType<LocalDataSourceBuilder>().Keyed<IDataSourceBuilder>(DataSourceType.Local);
            builder.RegisterType<GoogleDriveDataSourceBuilder>().Keyed<IDataSourceBuilder>(DataSourceType.GoogleDrive);

            builder.RegisterType<DataSourceCollection>().As<IDataSourceCollection>().SingleInstance();
            builder.RegisterType<DataSourceCreator>().As<IDataSourceCreator>();

            builder.RegisterWithInterfaceAndFuncFactory<DataSource, IDataSource>();
            builder.RegisterWithInterfaceAndFuncFactory<LibraryRepository, ILibraryRepository>();
            builder.RegisterWithInterfaceAndFuncFactory<ImageFileRepository, IImageFileRepository>();

            builder.RegisterAsSelfWithFuncFactory<LocalImageFileProvider>();
            builder.RegisterAsSelfWithFuncFactory<GoogleDriveImageFileProvider>();
            builder.RegisterAsSelfWithFuncFactory<LocalLibraryProvider>();
            builder.RegisterAsSelfWithFuncFactory<GoogleDriveLibraryProvider>();
            builder.RegisterAsSelfWithFuncFactory<LibraryQueryBuilder>();
            builder.RegisterAsSelfWithFuncFactory<ImageFileQueryBuilder>();
        }

        private void RegisterRemoteStorageInfos(ContainerBuilder builder)
        {
            builder.RegisterAsSelfWithFuncFactory<SerializableRemoteStorageInfo>();
            builder.RegisterAsSelfWithFuncFactory<GoogleDriveDataStoreInfo>();

            builder.RegisterType<GoogleDriveDataStoreInfo>().Keyed<IDataStoreInfo>(DataSourceType.GoogleDrive);
        }
    }
}
