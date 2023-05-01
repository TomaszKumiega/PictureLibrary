using PictureLibraryModel.Model.Settings;

namespace PictureLibrary.Settings.LibrarySettings
{
    public interface ILibrarySettingsProvider
    {
        LibrariesSettings GetLibrarySettings();
    }
}
