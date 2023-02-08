using System;

namespace PictureLibraryModel.Model
{
    public class LocalDataStoreInfo : IDataStoreInfo
    {
        public string Name { get; set; }
        public Guid Id => Guid.Parse("0773F5ED-C4BC-4034-AC20-C9DF6F744FD9");

        public DataSourceType DataSourceType => DataSourceType.Local;

        public void Deserialize(string serializedDataStoreInfo)
            => throw new InvalidOperationException("There is no need to deserialize local data store info.");

        public string Serialize()
            => throw new InvalidOperationException("There is no need to serialize local data store info.");
    }
}
