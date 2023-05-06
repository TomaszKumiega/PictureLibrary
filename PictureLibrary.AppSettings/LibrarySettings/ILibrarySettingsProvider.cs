using PictureLibraryModel.Model;

namespace PictureLibrary.AppSettings.LibrarySettings
{
    public interface ILibrarySettingsProvider
    {
        LibrariesSettings GetLibrarySettings();
        bool SaveLibrariesSettings();
    }
}
