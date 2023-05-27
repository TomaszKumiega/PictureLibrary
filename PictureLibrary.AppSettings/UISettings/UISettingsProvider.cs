using PictureLibrary.FileSystem.API;
using PictureLibraryModel.Model;
using System.Drawing;

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

        protected override UISettings CreateDefaultSettings()
        {
            return new UISettings()
            {
                LightMode = false,
            };
        }
    }
}
