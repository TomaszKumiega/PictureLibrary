using PictureLibraryModel.Model;

namespace PictureLibraryModel.DataProviders
{
    public class DataSource : IDataSource
    {
        public IRemoteStorageInfo RemoteStorageInfo { get; set; }
        public IImageFileProvider ImageProvider { get; set; }
        public ILibraryProvider LibraryProvider { get; set; }
    }
}
