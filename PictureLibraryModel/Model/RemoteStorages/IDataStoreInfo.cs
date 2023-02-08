using System;

namespace PictureLibraryModel.Model
{
    public interface IDataStoreInfo
    {
        Guid Id { get; }
        string Name { get; }
        DataSourceType DataSourceType { get; }

        void Deserialize(string serializedDataStoreInfo);
        string Serialize();
    }
}
