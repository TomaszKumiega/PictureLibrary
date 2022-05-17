using System;

namespace PictureLibraryModel.Model.LibraryModel
{
    public abstract class RemoteLibrary : Library
    {
        public Guid RemoteStorageInfoId { get; set; }
    }
}
