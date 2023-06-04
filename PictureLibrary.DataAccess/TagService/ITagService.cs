using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.TagService
{
    public interface ITagService
    {
        Task AddTagAsync(Library library, Tag tag);
        Task<IEnumerable<Tag>> GetAllTagsAsync(Library library);
        Task<bool> DeleteTagAsync(Library library, Tag tag);
        Task<bool> UpdateTagAsync(Library library, Tag tag);
    }
}
