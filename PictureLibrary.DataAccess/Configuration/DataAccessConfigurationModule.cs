﻿using Autofac;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.LibraryService;
using PictureLibrary.Tools.Extensions;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess.Configuration
{
    public class DataAccessConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataStoreInfoService>().As<IDataStoreInfoService>().SingleInstance();
            builder.RegisterTypeWithLocator<FileSystemLibraryService, IFileSystemLibraryService>();
            builder.RegisterTypeWithLocator<GoogleDriveLibraryService, IGoogleDriveLibraryService>();
            builder.Register<Func<IPictureLibraryAPILibraryService>>(ctx =>
            {
                return () => null;
            });

            builder.RegisterType<FileSystemLibraryService>().As<ILibraryService>().Keyed<DataStoreType>(DataStoreType.Local);
            builder.RegisterType<GoogleDriveLibraryService>().As<ILibraryService>().Keyed<DataStoreType>(DataStoreType.GoogleDrive);

            builder.RegisterType<LibrariesProvider>().As<ILibrariesProvider>().SingleInstance();
        }
    }
}
