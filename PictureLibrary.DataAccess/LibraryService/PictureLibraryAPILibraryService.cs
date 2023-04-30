using PictureLibrary.APIClient.LibraryClient;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibrary.DataAccess.LibraryService
{
    public class PictureLibraryAPILibraryService : ILibraryService<ApiLibrary>
    {
        private readonly ILibraryClient _libraryClient;
        private readonly IDataStoreInfoProvider _dataStoreInfoProvider;

        public PictureLibraryAPILibraryService(
            ILibraryClient libraryClient,
            IDataStoreInfoProvider dataStoreInfoProvider)
        {
            _libraryClient = libraryClient;
            _dataStoreInfoProvider = dataStoreInfoProvider;
        }

        public async Task<ApiLibrary> AddLibraryAsync(ApiLibrary library)
        {
            APIDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId);

            return await _libraryClient.AddLibraryAsync(dataStoreInfo, library);
        }

        public async Task<bool> DeleteLibraryAsync(ApiLibrary library)
        {
            APIDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId);

            return await _libraryClient.DeleteLibraryAsync(dataStoreInfo, library);
        }

        public async Task<IEnumerable<ApiLibrary>> GetAllLibrariesAsync(APIDataStoreInfo dataStoreInfo)
        {
            return await _libraryClient.GetAllLibrariesAsync(dataStoreInfo);
        }

        public async Task UpdateLibraryAsync(ApiLibrary library)
        {
            APIDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId);

            await _libraryClient.UpdateLibraryAsync(dataStoreInfo, library);
        }
    }
}
