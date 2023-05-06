using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.APIClient.UserClient
{
    public class UserClient : IUserClient
    {
        public Task<APIUser> AddUserAsync(APIDataStoreInfo apiDataStoreInfo, APIUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(APIDataStoreInfo apiDataStoreInfo, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<APIUser>> FindUsersByUsernameAsync(APIDataStoreInfo apiDataStoreInfo, string username)
        {
            throw new NotImplementedException();
        }

        public Task<APIUser> GetUserAsync(APIDataStoreInfo apiDataStoreInfo, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<APIDataStoreInfo> Login(APIDataStoreInfo apiDataStoreInfo, string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<APIDataStoreInfo> RefreshTokens(APIDataStoreInfo apiDataStoreInfo, APIUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(APIDataStoreInfo apiDataStoreInfo, APIUser user)
        {
            throw new NotImplementedException();
        }
    }
}
