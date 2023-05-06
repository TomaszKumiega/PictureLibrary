using PictureLibraryModel.Model;

namespace PictureLibrary.AppSettings
{
    public interface IUISettingsProvider
    {
        UISettings? GetUISettings();
        void SaveUISettings();
    }
}