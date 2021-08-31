using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model.RemoteStorages
{
    public interface IRemoteStorageInfo
    {
        Guid Id { get; set; }
        string Name { get; }
        StorageTypes StorageType { get; set; }

        void LoadFromString(string serializedStorageInfo);
        string ToString();
    }
}
