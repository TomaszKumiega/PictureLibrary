using PictureLibraryModel.Model.Settings;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.SettingsProvider
{
    public interface ISettingsProvider
    {
        Settings Settings { get; }

        Task SaveSettingsAsync();
    }
}