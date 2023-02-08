using Newtonsoft.Json;
using System;

namespace PictureLibraryModel.Model
{
    public class GoogleDriveDataStoreInfo : IDataStoreInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

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
