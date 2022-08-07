using System;

namespace PictureLibraryModel.Model.Builders
{
    public interface IRemoteLibraryBuilder : IGenericLibraryBuilder<IRemoteLibraryBuilder>
    {
        IRemoteLibraryBuilder WithRemoteStorageInfo(Guid? remoteStorageInfoId);
    }
}
