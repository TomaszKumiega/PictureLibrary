using System.Collections.Generic;

namespace PictureLibraryModel.Model.Settings
{
    public class SettingsBase
    {
        public bool LightMode { get; set; }
        public string AccentColor { get; set; }
        public string Language { get; set; }
        public List<string> ImportedLocalLibraries { get; set; }
    }
}
