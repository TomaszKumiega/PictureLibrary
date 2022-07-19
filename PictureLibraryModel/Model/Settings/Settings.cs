using System.Collections.Generic;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model.Settings
{
    public class Settings : SettingsBase
    {
        [XmlIgnore]
        public IEnumerable<IRemoteStorageInfo> RemoteStorageInfos { get; set; }
    }
}
