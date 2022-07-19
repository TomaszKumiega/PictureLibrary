using PictureLibraryModel.Model;

namespace PictureLibraryModel.DataProviders.Builders
{
    public interface IDataSourceCreator
    {
        IDataSource CreateDataSource(IRemoteStorageInfo remoteStorageInfo = null);
    }
}
