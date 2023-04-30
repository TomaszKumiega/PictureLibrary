using Newtonsoft.Json;
using System;

namespace PictureLibraryModel.Model
{
    public abstract class RemoteLibrary : Library, IRemoteEntity
    {
        [JsonIgnore]
        public Guid DataStoreInfoId { get; set; }
    }
}
