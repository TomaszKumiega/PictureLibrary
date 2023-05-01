using Newtonsoft.Json;
using System;

namespace PictureLibraryModel.Model.DataStoreInfo
{
    public class GoogleDriveDataStoreInfo : IDataStoreInfo
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required string UserName { get; set; }

        public void Deserialize(string serializedStorageInfo)
        {
            var googleDriveRemoteStorageInfo = JsonConvert.DeserializeObject<GoogleDriveDataStoreInfo>(serializedStorageInfo);

            if (googleDriveRemoteStorageInfo == null)
                return;

            Id = googleDriveRemoteStorageInfo.Id;
            Name = googleDriveRemoteStorageInfo.Name;
            UserName = googleDriveRemoteStorageInfo.UserName;
        }

        public string Serialize()
            => JsonConvert.SerializeObject(this);
    }
}
