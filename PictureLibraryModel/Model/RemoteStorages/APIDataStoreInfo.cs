﻿using Newtonsoft.Json;
using System;

namespace PictureLibraryModel.Model.RemoteStorages
{
    public class APIDataStoreInfo : IDataStoreInfo
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required Guid TokenId { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime ExpiryDate { get; set; }

        [JsonIgnore]
        public DataSourceType DataSourceType
            => DataSourceType.PictureLibraryAPI;

        public void Deserialize(string serializedDataStoreInfo)
        {
            var storeInfo = (APIDataStoreInfo?)JsonConvert.DeserializeObject(serializedDataStoreInfo);

            if (storeInfo != null)
            {
                Id = storeInfo.Id;
                Name = storeInfo.Name;
                TokenId = storeInfo.TokenId;
                AccessToken = storeInfo.AccessToken;
                RefreshToken = storeInfo.RefreshToken;
                ExpiryDate = storeInfo.ExpiryDate;
            }
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
