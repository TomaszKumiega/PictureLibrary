using System;

namespace PictureLibraryModel.Model
{
    public abstract class RemoteImageFile : ImageFile, IRemoteEntity
    {
        protected RemoteImageFile() : base() 
        { 
        }

        public virtual Guid DataStoreInfoId { get; set; }
    }
}
