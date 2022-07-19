using PictureLibraryModel.Model;

namespace PictureLibraryModel.DataProviders
{
    public interface IDataSource
    {
        IImageFileProvider ImageProvider { get; set; }
        ILibraryProvider LibraryProvider { get; set; }
        IRemoteStorageInfo RemoteStorageInfo { get; set; }
    }
}