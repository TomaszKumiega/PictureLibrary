using System;

namespace PictureLibraryModel.Model.FileSystemModel
{
    public abstract class RemoteImageFile : ImageFile
    {
        public virtual Guid RemoteStorageInfoId { get; }
    }
}
