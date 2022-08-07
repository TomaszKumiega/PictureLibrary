using System.Collections.Generic;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model.Settings
{
    public class Settings : SettingsBase
    {
        [XmlIgnore]
        public IList<IRemoteStorageInfo> RemoteStorageInfos { get; set; }
    }
}
