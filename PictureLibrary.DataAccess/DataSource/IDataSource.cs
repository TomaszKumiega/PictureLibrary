using PictureLibrary.DataAccess.ImageProvider;
using PictureLibrary.DataAccess.LibraryProvider;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.DataSource
{
    public interface IDataSource
    {
        IImageFileProvider? ImageProvider { get; set; }
        ILibraryProvider? LibraryProvider { get; set; }
        IDataStoreInfo? RemoteStorageInfo { get; set; }
    }
}