﻿using PictureLibrary.DataAccess.DataSource;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Builders
{
    public interface IDataSourceBuilder
    {
        IDataSourceBuilder CreateDataSource();
        IDataSourceBuilder WithLibraryProvider();
        IDataSourceBuilder WithImageFileProvider();
        IDataSourceBuilder WithRemoteStorageInfo(IRemoteStorageInfo? remoteStorageInfo);
        IDataSource Build();
    }
}
