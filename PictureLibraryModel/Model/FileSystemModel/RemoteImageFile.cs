using System;

namespace PictureLibraryModel.Model.FileSystemModel
{
    public abstract class RemoteImageFile : ImageFile, IRemoteEntity
    {
        protected RemoteImageFile() : base() 
        { 
        }

        public virtual Guid RemoteStorageInfoId { get; set; }
    }
}
