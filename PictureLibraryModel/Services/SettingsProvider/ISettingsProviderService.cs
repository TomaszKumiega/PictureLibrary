using PictureLibraryModel.Model.Settings;

namespace PictureLibraryModel.Services.SettingsProvider
{
    public interface ISettingsProviderService
    {
        Settings Settings { get; }

        void SaveSettings();
    }
}