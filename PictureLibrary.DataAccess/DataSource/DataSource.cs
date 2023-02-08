using PictureLibrary.DataAccess.ImageProvider;
using PictureLibrary.DataAccess.LibraryProvider;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.DataSource
{
    public class DataSource : IDataSource
    {
        public IDataStoreInfo? RemoteStorageInfo { get; set; }
        public IImageFileProvider? ImageProvider { get; set; }
        public ILibraryProvider? LibraryProvider { get; set; }
    }
}
