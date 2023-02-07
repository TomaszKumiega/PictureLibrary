using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;

namespace PictureLibraryModel.Services.RemoteStorageInfoSerializer
{
    public class RemoteStorageInfosSerializer : IRemoteStorageInfosSerializer
    {
        private readonly IImplementationSelector<DataSourceType, IRemoteStorageInfo> _remoteStorageInfoImplementationSelector;
        private readonly Func<SerializableRemoteStorageInfo> _serializableRemoteStorageInfoLocator;

        public RemoteStorageInfosSerializer(
            IImplementationSelector<DataSourceType, IRemoteStorageInfo> remoteStorageInfoImplementationSelector, 
            Func<SerializableRemoteStorageInfo> serializableRemoteStorageInfoLocator)
        {
            _remoteStorageInfoImplementationSelector = remoteStorageInfoImplementationSelector;
            _serializableRemoteStorageInfoLocator = serializableRemoteStorageInfoLocator;
        }

        public List<IRemoteStorageInfo> DeserializeStorageInfos(List<SerializableRemoteStorageInfo> serializedRemoteStorageInfos)
        {
            var remoteStorageInfos = new List<IRemoteStorageInfo>();

            foreach (var serializedRemoteStorageInfo in serializedRemoteStorageInfos)
            {
                var remoteStorageInfo = _remoteStorageInfoImplementationSelector.Select(serializedRemoteStorageInfo.DataSourceType);

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
                serializableRemoteStorageInfo.DataSourceType = remoteStorageInfo.DataSourceType;
                serializableRemoteStorageInfo.SerializedRemoteStorageInfo = remoteStorageInfo.ToString();

                serializedRemoteStorageInfos.Add(serializableRemoteStorageInfo);
            }

            return serializedRemoteStorageInfos;
        }
    }
}
