using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibraryModel.DataProviders.Builders
{
    public interface IDataSourceBuilder
    {
        IDataSourceBuilder CreateLibraryProvider();
        IDataSourceBuilder CreateImageFileProvider();
        IDataSourceBuilder WithRemoteStorageInfo(IRemoteStorageInfo remoteStorageInfo);
        IDataSource Build();
    }
}
