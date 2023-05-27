using PictureLibrary.FileSystem.API;
using PictureLibraryModel.Model;

namespace PictureLibrary.AppSettings
{
    public class LibrarySettingsProvider : SettingsProviderBase<LibrariesSettings>, ILibrarySettingsProvider
    {
        public LibrarySettingsProvider(
            IPathFinder pathFinder, 
            IFileService fileService) 
            : base(pathFinder, fileService)
        {
        }

        protected override LibrariesSettings CreateDefaultSettings()
        {
            return new LibrariesSettings()
            {
                LocalLibrariesStoragePath = _pathFinder.GetDefaultLibrariesFolderPath(),
            };
        }
    }
}
