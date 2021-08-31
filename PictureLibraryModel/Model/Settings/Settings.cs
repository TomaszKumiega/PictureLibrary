using PictureLibraryModel.Model.RemoteStorages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model.Settings
{
    public class Settings 
    {
        public bool LightMode { get; set; }
        public string AccentColor { get; set; }
        public string Language { get; set; }
        public List<string> ImportedLibraries { get; set; }
        public List<IRemoteStorageInfo> RemoteStorageInfos { get; set; }
    }
}
