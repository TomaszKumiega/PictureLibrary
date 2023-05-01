using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibrary.APIClient.UserClient
{
    public interface IUserClient
    {
        Task<APIUser> AddUserAsync(APIDataStoreInfo apiDataStoreInfo, APIUser user);
        Task UpdateUserAsync(APIDataStoreInfo apiDataStoreInfo, APIUser user);
        Task<APIUser> GetUserAsync(APIDataStoreInfo apiDataStoreInfo, Guid userId);
        Task<IEnumerable<APIUser>> FindUsersByUsernameAsync(APIDataStoreInfo apiDataStoreInfo, string username);
        Task<bool> DeleteUserAsync(APIDataStoreInfo apiDataStoreInfo, Guid userId);
        Task<APIDataStoreInfo> Login(APIDataStoreInfo apiDataStoreInfo, string username, string password);
        Task<APIDataStoreInfo> RefreshTokens(APIDataStoreInfo apiDataStoreInfo, APIUser user);
    }
}
