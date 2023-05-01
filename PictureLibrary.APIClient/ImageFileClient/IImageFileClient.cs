using PictureLibraryModel.Model;

namespace PictureLibrary.APIClient.ImageFileClient
{
    public interface IImageFileClient
    {
        Task<IEnumerable<ApiImageFile>> GetAllImageFilesAsync(Guid libraryId);
        Task<Stream> GetFileAsync(Guid imageFileId);
        Task UploadFileAsync(Stream stream, string fileName, IEnumerable<Guid> libraryIds);
        Task<bool> DeleteImageFileAsync(Guid imageFileId);
        Task UpdateImageFileAsync(ApiImageFile imageFile);
        Task UpdateFileContentAsync(ApiImageFile imageFile, Stream contentStream);
    }
}
