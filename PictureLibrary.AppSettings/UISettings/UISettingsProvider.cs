using PictureLibrary.FileSystem.API;
using PictureLibraryModel.Model;

namespace PictureLibrary.AppSettings
{
    public class UISettingsProvider : SettingsProviderBase<UISettings>, IUISettingsProvider
    {
        public UISettingsProvider(
            IPathFinder pathFinder, 
            IFileService fileService) 
            : base(pathFinder, fileService)
        {
        }
    }
}
