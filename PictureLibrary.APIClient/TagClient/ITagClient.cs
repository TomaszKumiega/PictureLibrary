using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibrary.APIClient.TagClient
{
    public interface ITagClient
    {
        Task<Tag> AddTagAsync(APIDataStoreInfo dataStoreInfo, Tag tag, Guid libraryId);
        Task<IEnumerable<Tag>> GetAllTagsAsync(APIDataStoreInfo dataStoreInfo, Guid libraryId);
        Task<bool> DeleteTagAsync(APIDataStoreInfo dataStoreInfo, Guid tagId);
        Task<bool> UpdateTagAsync(APIDataStoreInfo dataStoreInfo, Tag tag);
    }
}
