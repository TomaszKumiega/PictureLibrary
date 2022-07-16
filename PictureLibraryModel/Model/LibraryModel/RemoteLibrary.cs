using System;

namespace PictureLibraryModel.Model
{
    public abstract class RemoteLibrary : Library
    {
        public Guid RemoteStorageInfoId { get; set; }
    }
}
