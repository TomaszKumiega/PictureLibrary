using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model.RemoteStorages;
using System;
using System.Collections.Generic;

namespace PictureLibraryModel.Services.RemoteStorageInfoSerializer
{
    public class RemoteStorageInfosSerializer : IRemoteStorageInfosSerializer
    {
        private readonly IImplementationSelector<RemoteStorageType, IRemoteStorageInfo> _remoteStorageInfoImplementationSelector;
        private readonly Func<SerializableRemoteStorageInfo> _serializableRemoteStorageInfoLocator;

        public RemoteStorageInfosSerializer(
            IImplementationSelector<RemoteStorageType, IRemoteStorageInfo> remoteStorageInfoImplementationSelector, 
            Func<SerializableRemoteStorageInfo> serializableRemoteStorageInfoLocator)
        {
            _remoteStorageInfoImplementationSelector = remoteStorageInfoImplementationSelector;
            _serializableRemoteStorageInfoLocator = serializableRemoteStorageInfoLocator;
        }

        public IEnumerable<IRemoteStorageInfo> DeserializeStorageInfos(List<SerializableRemoteStorageInfo> serializedRemoteStorageInfos)
        {
            var remoteStorageInfos = new List<IRemoteStorageInfo>();

            foreach (var serializedRemoteStorageInfo in serializedRemoteStorageInfos)
            {
                var remoteStorageInfo = _remoteStorageInfoImplementationSelector.Select(serializedRemoteStorageInfo.RemoteStorageType);

                remoteStorageInfo.Deserialize(serializedRemoteStorageInfo.SerializedRemoteStorageInfo);

                remoteStorageInfos.Add(remoteStorageInfo);
            }

            return remoteStorageInfos;
        }

        public List<SerializableRemoteStorageInfo> SerializeStorageInfos(IEnumerable<IRemoteStorageInfo> remoteStorageInfos)
        {
            var serializedRemoteStorageInfos = new List<SerializableRemoteStorageInfo>();

            foreach (var remoteStorageInfo in remoteStorageInfos)
            {
                var serializableRemoteStorageInfo = _serializableRemoteStorageInfoLocator();
                serializableRemoteStorageInfo.RemoteStorageType = remoteStorageInfo.StorageType;
                serializableRemoteStorageInfo.SerializedRemoteStorageInfo = remoteStorageInfo.ToString();

                serializedRemoteStorageInfos.Add(serializableRemoteStorageInfo);
            }

            return serializedRemoteStorageInfos;
        }
    }
}
