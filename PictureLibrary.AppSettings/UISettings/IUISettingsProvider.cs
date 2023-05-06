using PictureLibraryModel.Model;

namespace PictureLibrary.AppSettings
{
    public interface IUISettingsProvider
    {
        UISettings? GetSettings();
        bool SaveSettings(UISettings settings);
    }
}