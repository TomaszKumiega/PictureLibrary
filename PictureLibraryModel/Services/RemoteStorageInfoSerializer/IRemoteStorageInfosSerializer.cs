using PictureLibraryModel.Model;
using System.Collections.Generic;

namespace PictureLibraryModel.Services.RemoteStorageInfoSerializer
{
    public interface IRemoteStorageInfosSerializer
    {
        List<SerializableRemoteStorageInfo> SerializeStorageInfos(IEnumerable<IRemoteStorageInfo> remoteStorageInfos);
        IEnumerable<IRemoteStorageInfo> DeserializeStorageInfos(List<SerializableRemoteStorageInfo> serializedRemoteStorageInfos);
    }
}
