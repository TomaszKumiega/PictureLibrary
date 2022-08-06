using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public interface IRemoteStorageInfo
    {
        Guid Id { get; set; }
        string Name { get; }
        DataSourceType DataSourceType { get; }

        void Deserialize(string serializedStorageInfo);

        string ToString();
    }
}
