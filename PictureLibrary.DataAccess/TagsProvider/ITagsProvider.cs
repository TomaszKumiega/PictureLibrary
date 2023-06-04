using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess
{
    public interface ITagsProvider
    {
        Task<IEnumerable<Tag>> GetTagsFromLibraryAsync(Library library);
    }
}