using System;

namespace PictureLibraryModel.Model.DataStoreInfo
{
    public class GoogleDriveDataStoreInfo : IDataStoreInfo
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }

        public required string UserName { get; set; }
    }
}
