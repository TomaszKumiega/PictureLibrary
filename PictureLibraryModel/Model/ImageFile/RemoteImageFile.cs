using System;

namespace PictureLibraryModel.Model
{
    public abstract class RemoteImageFile : ImageFile, IRemoteEntity
    {
        public virtual Guid DataStoreInfoId { get; set; }

        protected RemoteImageFile() : base() 
        { 
        }
    }
}
