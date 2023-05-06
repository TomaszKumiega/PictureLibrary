//using PictureLibrary.APIClient.LibraryClient;
//using PictureLibrary.DataAccess.DataStoreInfos;
//using PictureLibrary.DataAccess.Exceptions;
//using PictureLibraryModel.Model;
//using PictureLibraryModel.Model.DataStoreInfo;

//namespace PictureLibrary.DataAccess.LibraryService
//{
//    public class PictureLibraryAPILibraryService
//    {
//        #region Private fields
//        private readonly ILibraryClient _libraryClient;
//        private readonly IDataStoreInfoService _dataStoreInfoProvider;
//        #endregion

//        public PictureLibraryAPILibraryService(
//            ILibraryClient libraryClient,
//            IDataStoreInfoService dataStoreInfoProvider)
//        {
//            _libraryClient = libraryClient;
//            _dataStoreInfoProvider = dataStoreInfoProvider;
//        }

//        #region Public methods
//        public async Task<ApiLibrary> AddLibraryAsync(ApiLibrary library)
//        {
//            APIDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();

//            return await _libraryClient.AddLibraryAsync(dataStoreInfo, library);
//        }

//        public async Task<bool> DeleteLibraryAsync(ApiLibrary library)
//        {
//            APIDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();

//            return await _libraryClient.DeleteLibraryAsync(dataStoreInfo, library);
//        }

//        public async Task<IEnumerable<ApiLibrary>> GetAllLibrariesAsync(APIDataStoreInfo dataStoreInfo)
//        {
//            return await _libraryClient.GetAllLibrariesAsync(dataStoreInfo);
//        }

//        public async Task UpdateLibraryAsync(ApiLibrary library)
//        {
//            APIDataStoreInfo dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId) ?? throw new GoogleDriveAccountConfigurationNotFoundException();

//            await _libraryClient.UpdateLibraryAsync(dataStoreInfo, library);
//        }
//        #endregion
//    }
//}
