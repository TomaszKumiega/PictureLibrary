using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.LibraryService;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess
{
    public class LibrariesProvider : ILibrariesProvider
    {
        private readonly IDataStoreInfoService _dataStoreInfoService;
        private readonly Func<IFileSystemLibraryService> _fileSystemLibraryServiceLocator;
        private readonly Func<IGoogleDriveLibraryService> _googleDriveLibraryServiceLocator;
        private readonly Func<IPictureLibraryAPILibraryService> _pictureLibraryApiLibraryServiceLocator;

        public LibrariesProvider(
            IDataStoreInfoService dataStoreInfoService,
            Func<IFileSystemLibraryService> fileSystemLibraryServiceLocator,
            Func<IGoogleDriveLibraryService> googleDriveLibraryServiceLocator,
            Func<IPictureLibraryAPILibraryService> pictureLibraryApiLibraryServiceLocator)
        {
            _dataStoreInfoService = dataStoreInfoService;
            _fileSystemLibraryServiceLocator = fileSystemLibraryServiceLocator;
            _googleDriveLibraryServiceLocator = googleDriveLibraryServiceLocator;
            _pictureLibraryApiLibraryServiceLocator = pictureLibraryApiLibraryServiceLocator;
        }

        public async Task<IEnumerable<Library>> GetLibrariesFromAllSourcesAsync()
        {
            var pictureLibraryApiDataStoreInfos = _dataStoreInfoService.GetAllDataStoreInfosOfType<APIDataStoreInfo>();
            var googleDriveDataStoreInfos = _dataStoreInfoService.GetAllDataStoreInfosOfType<GoogleDriveDataStoreInfo>();

            var libraries = new List<Library>();
            
            
            libraries.AddRange(await GetAllLocalLibrariesAsync());
            libraries.AddRange(await GetAllPictureLibraryApiLibrariesAsync(pictureLibraryApiDataStoreInfos));
            libraries.AddRange(await GetAllGoogleDriveLibrariesAsync(googleDriveDataStoreInfos));

            return libraries;
        }

        private async Task<IEnumerable<Library>> GetAllLocalLibrariesAsync()
        {
            var fileSystemLibraryService = _fileSystemLibraryServiceLocator();
            return await fileSystemLibraryService.GetAllLibrariesAsync();
        }

        private async Task<IEnumerable<Library>> GetAllPictureLibraryApiLibrariesAsync(IEnumerable<APIDataStoreInfo> apiDataStoreInfos)
        {
            if (apiDataStoreInfos?.Any() == true)
                return Enumerable.Empty<Library>();

            var pictureLibraryApiLibraryService = _pictureLibraryApiLibraryServiceLocator();
            var libraries = new List<Library>();

            foreach (var dataStoreInfo in apiDataStoreInfos!)
            {
                try
                {
                    libraries.AddRange(await pictureLibraryApiLibraryService.GetAllLibrariesAsync(dataStoreInfo));
                }
                catch (Exception)
                {
                    //TODO: logging
                    throw;
                }
            }

            return libraries;
        }

        private async Task<IEnumerable<Library>> GetAllGoogleDriveLibrariesAsync(IEnumerable<GoogleDriveDataStoreInfo> googleDriveDataStoreInfos)
        {
            if (googleDriveDataStoreInfos?.Any() == true)
                return Enumerable.Empty<Library>();

            var googleDriveLibraryService = _googleDriveLibraryServiceLocator();
            var libraries = new List<Library>();

            foreach (var dataStoreInfo in googleDriveDataStoreInfos!)
            {
                try
                {
                    libraries.AddRange(await googleDriveLibraryService.GetAllLibrariesAsync(dataStoreInfo));
                }
                catch (Exception)
                {
                    //TODO: logging
                    throw;
                }
            }

            return libraries;
        }
    }
}
