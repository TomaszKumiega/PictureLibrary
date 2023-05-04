using Newtonsoft.Json;
using System;

namespace PictureLibraryModel.Model.DataStoreInfo
{
    public class APIDataStoreInfo : IDataStoreInfo
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public required Guid UserId { get; set; }
        public required Guid TokenId { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime ExpiryDate { get; set; }
    }
}
