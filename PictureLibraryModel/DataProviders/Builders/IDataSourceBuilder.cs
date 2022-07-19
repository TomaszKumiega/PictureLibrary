﻿using PictureLibraryModel.Model;

namespace PictureLibraryModel.DataProviders.Builders
{
    public interface IDataSourceBuilder
    {
        IDataSourceBuilder CreateDataSource();
        IDataSourceBuilder WithLibraryProvider();
        IDataSourceBuilder WithImageFileProvider();
        IDataSourceBuilder WithRemoteStorageInfo(IRemoteStorageInfo remoteStorageInfo);
        IDataSource Build();
    }
}
