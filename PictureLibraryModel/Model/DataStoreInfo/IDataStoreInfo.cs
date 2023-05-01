using System;

namespace PictureLibraryModel.Model.DataStoreInfo
{
    public interface IDataStoreInfo
    {
        Guid Id { get; }
        string Name { get; }

        void Deserialize(string serializedDataStoreInfo);
        string Serialize();
    }
}
