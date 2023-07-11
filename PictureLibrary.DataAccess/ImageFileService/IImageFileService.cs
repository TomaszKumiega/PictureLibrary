using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.ImageFileService
{
    public interface IImageFileService
    {
        Task AddImageFile(ImageFile imageFile, Stream imageFileContent, Library library);
        Task<IEnumerable<ImageFile>> GetAllImageFiles(Library library);
        Task DeleteImageFile(ImageFile imageFile, Library library);
        Task UpdateImageFile(ImageFile imageFile, Library library);
        Task<Stream> GetFileContent(ImageFile imageFile);
        Task UpdateFileContent(ImageFile imageFile, Stream newFileContentStream);
    }
}
