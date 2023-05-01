using PictureLibraryModel.Model.Settings;

namespace PictureLibraryModel.Services.SettingsProvider
{
    public interface ISettingsProvider
    {
        Settings? Settings { get; }

        void SaveSettings();
    }
}