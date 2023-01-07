using System;

namespace PictureLibraryModel.Model
{
    public interface IRemoteEntity
    {
        Guid RemoteStorageInfoId { get; set; }
    }
}
