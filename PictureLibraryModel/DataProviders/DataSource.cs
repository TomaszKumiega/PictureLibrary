using PictureLibraryModel.Model.RemoteStorages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.DataProviders
{
    public class DataSource : IDataSource
    {
        public bool IsLocalDataSource { get; }
        public IRemoteStorageInfo RemoteStorageInfo { get; }
        public IImageProvider ImageProvider { get; }
        public ILibraryProvider LibraryProvider { get; }
    }
}
