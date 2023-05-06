//using PictureLibrary.APIClient.UserClient;
//using PictureLibrary.DataAccess.DataStoreInfos;
//using PictureLibrary.DataAccess.Exceptions;
//using PictureLibraryModel.Model;
//using PictureLibraryModel.Model.DataStoreInfo;

//namespace PictureLibrary.DataAccess.UserService
//{
//    public class PictureLibraryApiUserService
//    {
//        #region Private fields
//        private readonly IUserClient _userClient;
//        private readonly IDataStoreInfoService _dataStoreInfoProvider;
//        #endregion

//        public PictureLibraryApiUserService(
//            IUserClient userClient,
//            IDataStoreInfoService dataStoreInfoProvider)
//        {
//            _userClient = userClient;
//            _dataStoreInfoProvider = dataStoreInfoProvider;
//        }

//        #region Public methods
//        public async Task<APIUser> AddUserAsync(APIUser user)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(user.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            return await _userClient.AddUserAsync(dataStoreInfo, user);
//        }

//        public async Task UpdateUserAsync(APIUser user)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(user.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            await _userClient.UpdateUserAsync(dataStoreInfo, user);
//        }

//        public async Task<APIUser> GetUserAsync(APIUser user)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(user.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            return await _userClient.GetUserAsync(dataStoreInfo, user.Id);
//        }

//        public async Task<bool> DeleteUserAsync(APIUser user)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(user.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            return await _userClient.DeleteUserAsync(dataStoreInfo, user.Id);
//        }

//        public async Task<IEnumerable<APIUser>> FindUsersByUsernameAsync(Guid dataStoreInfoId, string username)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(dataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            return await _userClient.FindUsersByUsernameAsync(dataStoreInfo, username);
//        }

//        public async Task<APIDataStoreInfo> LoginAsync(APIDataStoreInfo apiDataStoreInfo, string username, string password)
//        {
//            return await _userClient.Login(apiDataStoreInfo, username, password);
//        }

//        public async Task<APIDataStoreInfo> RefreshTokens(APIDataStoreInfo apiDataStoreInfo, APIUser user)
//        {
//            return await _userClient.RefreshTokens(apiDataStoreInfo, user);
//        }
//        #endregion
//    }
//}
