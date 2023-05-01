using PictureLibrary.APIClient.UserClient;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibrary.DataAccess.UserService
{
    public class PictureLibraryApiUserService
    {
        private readonly IUserClient _userClient;
        private readonly IDataStoreInfoProvider _dataStoreInfoProvider;

        public PictureLibraryApiUserService(
            IUserClient userClient,
            IDataStoreInfoProvider dataStoreInfoProvider)
        {
            _userClient = userClient;
            _dataStoreInfoProvider = dataStoreInfoProvider;
        }

        public async Task<APIUser> AddUserAsync(APIUser user)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(user.DataStoreInfoId);
            return await _userClient.AddUserAsync(dataStoreInfo, user);
        }

        public async Task UpdateUserAsync(APIUser user)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(user.DataStoreInfoId);
            await _userClient.UpdateUserAsync(dataStoreInfo, user);
        }

        public async Task<APIUser> GetUserAsync(APIUser user)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(user.DataStoreInfoId);
            return await _userClient.GetUserAsync(dataStoreInfo, user.Id);
        }

        public async Task<bool> DeleteUserAsync(APIUser user)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(user.DataStoreInfoId);
            return await _userClient.DeleteUserAsync(dataStoreInfo, user.Id);
        }

        public async Task<IEnumerable<APIUser>> FindUsersByUsernameAsync(Guid dataStoreInfoId, string username)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(dataStoreInfoId);
            return await _userClient.FindUsersByUsernameAsync(dataStoreInfo, username);
        }

        public async Task<APIDataStoreInfo> LoginAsync(APIDataStoreInfo apiDataStoreInfo, string username, string password)
        {
            return await _userClient.Login(apiDataStoreInfo, username, password);
        }

        public async Task<APIDataStoreInfo> RefreshTokens(APIDataStoreInfo apiDataStoreInfo, APIUser user)
        {
            return await _userClient.RefreshTokens(apiDataStoreInfo, user);
        }
    }
}
