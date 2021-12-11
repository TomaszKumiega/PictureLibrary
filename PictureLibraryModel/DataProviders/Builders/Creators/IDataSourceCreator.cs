using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibraryModel.DataProviders.Builders
{
    public interface IDataSourceCreator
    {
        IDataSource CreateDataSource(IRemoteStorageInfo remoteStorageInfo = null);
    }
}
