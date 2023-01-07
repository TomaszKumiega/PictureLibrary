using System;

namespace PictureLibraryModel.Model
{
    public abstract class RemoteLibrary : Library, IRemoteEntity
    {
        public Guid RemoteStorageInfoId { get; set; }
    }
}
