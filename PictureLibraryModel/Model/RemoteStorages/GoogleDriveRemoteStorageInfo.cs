using Newtonsoft.Json;
using System;

namespace PictureLibraryModel.Model
{
    public class GoogleDriveDataStoreInfo : IDataStoreInfo
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required string UserName { get; set; }

        [JsonIgnore]
        public DataSourceType DataSourceType => DataSourceType.GoogleDrive;

        public void Deserialize(string serializedStorageInfo)
        {
            var googleDriveRemoteStorageInfo = JsonConvert.DeserializeObject<GoogleDriveDataStoreInfo>(serializedStorageInfo);

            Id = googleDriveRemoteStorageInfo.Id;
            Name = googleDriveRemoteStorageInfo.Name;
            UserName = googleDriveRemoteStorageInfo.UserName;
        }

        public string Serialize()
            => JsonConvert.SerializeObject(this);
    }
}
