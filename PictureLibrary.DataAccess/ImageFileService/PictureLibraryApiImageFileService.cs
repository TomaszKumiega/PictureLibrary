using PictureLibrary.APIClient.ImageFileClient;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.ImageFileService
{
    public class PictureLibraryApiImageFileService
    {
        private readonly IImageFileClient _imageFileClient;

        public PictureLibraryApiImageFileService(
            IImageFileClient imageFileClient)
        {
            _imageFileClient = imageFileClient;
        }


        public async Task<IEnumerable<ApiImageFile>> GetApiImageFilesAsync(ApiLibrary library)
        {
            return await _imageFileClient.GetAllImageFilesAsync(library.Id);
        }

        public async Task<bool> DeleteImageFileAsync(ApiImageFile imageFile)
        {
            return await _imageFileClient.DeleteImageFileAsync(imageFile.Id);
        }

        public async Task UpdateImageFileAsync(ApiImageFile imageFile)
        {
            await _imageFileClient.UpdateImageFileAsync(imageFile);
        }

        public async Task<Stream> GetFileContentStreamAsync(ApiImageFile imageFile)
        {
            return await _imageFileClient.GetFileAsync(imageFile.Id);
        }

        public async Task UpdateFileContentAsync(ApiImageFile imageFile, Stream updatedImageFileContentStream)
        {
            await _imageFileClient.UpdateFileContentAsync(imageFile, updatedImageFileContentStream);
        }
    }
}
