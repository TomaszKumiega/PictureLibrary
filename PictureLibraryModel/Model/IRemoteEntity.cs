using System;

namespace PictureLibraryModel.Model
{
    public interface IRemoteEntity
    {
        Guid DataStoreInfoId { get; set; }
    }
}
