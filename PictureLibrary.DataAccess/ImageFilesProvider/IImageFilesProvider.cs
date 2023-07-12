using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess
{
    public interface IImageFilesProvider
    {
        Task<IEnumerable<ImageFile>> GetAllImageFilesFromLibraryAsync(Library library);
        Task<Stream> GetImageFileContent(ImageFile imageFile);
    }
}