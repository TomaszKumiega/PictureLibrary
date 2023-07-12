using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.ImageFileService;
using PictureLibrary.Infrastructure.ImplementationSelector;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess
{
    public class ImageFilesProvider : IImageFilesProvider
    {
        private readonly IDataStoreInfoService _dataStoreInfoService;
        private readonly IImplementationSelector<DataStoreType, IImageFileService> _imageFileServiceImplementationSelector;

        public ImageFilesProvider(
            IDataStoreInfoService dataStoreInfoService,
            IImplementationSelector<DataStoreType, IImageFileService> imageFileServiceImplementationSelector)
        {
            _dataStoreInfoService = dataStoreInfoService;
            _imageFileServiceImplementationSelector = imageFileServiceImplementationSelector;
        }

        public async Task<IEnumerable<ImageFile>> GetAllImageFilesFromLibraryAsync(Library library)
        {
            DataStoreType dataStoreType = _dataStoreInfoService.GetDataStoreTypeFromLibrary(library);

            var imageFileService = _imageFileServiceImplementationSelector.Select(dataStoreType);

            return await imageFileService.GetAllImageFiles(library);
        }

        public async Task<Stream> GetImageFileContent(ImageFile imageFile)
        {
            DataStoreType dataStoreType = _dataStoreInfoService.GetDataStoreTypeFromImageFile(imageFile);

            var imageFileService = _imageFileServiceImplementationSelector.Select(dataStoreType);

            return await imageFileService.GetFileContent(imageFile);
        }
    }
}
