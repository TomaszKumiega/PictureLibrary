using PictureLibraryModel.Model.Settings;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.SettingsProvider
{
    public interface ISettingsProviderService
    {
        Settings Settings { get; }

        Task SaveSettingsAsync();
    }
}