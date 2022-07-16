using System;

namespace PictureLibraryModel.Model.Builders
{
    public interface IRemoteLibraryBuilder : ILibraryBuilder
    {
        ILibraryBuilder WithStorageInfoId(Guid? storageInfoId);
    }
}
