using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model.RemoteStorages
{
    public interface IRemoteStorageInfo
    {
        Guid Id { get; set; }
        string Name { get; }
        DataSourceType DataSourceType { get; set; }

        void Deserialize(string serializedStorageInfo);

        string ToString();
    }
}
