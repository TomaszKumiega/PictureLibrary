using Newtonsoft.Json;
using System;

namespace PictureLibraryModel.Model.RemoteStorages
{
    public class GoogleDriveRemoteStorageInfo : IRemoteStorageInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        [JsonIgnore]
        public DataSourceType DataSourceType => DataSourceType.GoogleDrive;

        public void Deserialize(string serializedStorageInfo)
        {
            var googleDriveRemoteStorageInfo = JsonConvert.DeserializeObject<GoogleDriveRemoteStorageInfo>(serializedStorageInfo);

            Id = googleDriveRemoteStorageInfo.Id;
            Name = googleDriveRemoteStorageInfo.Name;
            UserName = googleDriveRemoteStorageInfo.UserName;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
