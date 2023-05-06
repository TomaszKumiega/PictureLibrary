using PictureLibraryModel.Model;

namespace PictureLibrary.AppSettings
{
    public interface ILibrarySettingsProvider
    {
        LibrariesSettings? GetSettings();
        bool SaveSettings(LibrariesSettings settings);
    }
}
