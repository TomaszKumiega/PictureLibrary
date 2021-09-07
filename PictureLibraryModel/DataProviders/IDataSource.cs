using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibraryModel.DataProviders
{
    public interface IDataSource
    {
        IImageProvider ImageProvider { get; }
        bool IsLocalDataSource { get; }
        ILibraryProvider LibraryProvider { get; }
        IRemoteStorageInfo RemoteStorageInfo { get; }
    }
}