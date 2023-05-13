//using PictureLibrary.APIClient.ImageFiles;
//using PictureLibrary.APIClient.Model.Authorization;
//using PictureLibrary.APIClient.Model.Requests;
//using PictureLibrary.DataAccess.DataStoreInfos;
//using PictureLibrary.DataAccess.Exceptions;
//using PictureLibraryModel.Model;
//using PictureLibraryModel.Model.DataStoreInfo;

//namespace PictureLibrary.DataAccess.ImageFileService
//{
//    public class PictureLibraryApiImageFileService
//    {
//        #region Private fields
//        private readonly IImageFileClient _imageFileClient;
//        private readonly IDataStoreInfoService _dataStoreInfoProvider;
//        #endregion

//        public PictureLibraryApiImageFileService(
//            IImageFileClient imageFileClient,
//            IDataStoreInfoService dataStoreInfoProvider)
//        {
//            _imageFileClient = imageFileClient;
//            _dataStoreInfoProvider = dataStoreInfoProvider;
//        }

//        #region Public methods
//        public async Task<Guid?> AddImageFileAsync(ApiImageFile imageFile, Stream imageContent, ApiLibrary library)
//        {
//            if (imageFile?.Name == null)
//                throw new ArgumentException(string.Empty, nameof(imageFile));

//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            var authorizationData = GetAuthorizationData(dataStoreInfo);

//            var reqeust = new AddImageFileRequest()
//            {
//                FileName = imageFile.Name,
//                Libraries = new List<Guid>() { library.Id },
//            };

//            return await _imageFileClient.AddImageFileAsync(authorizationData, reqeust, imageContent);
//        }

//        public async Task<IEnumerable<ApiImageFile>> GetApiImageFilesAsync(ApiLibrary library)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();

//            var authorizationData = GetAuthorizationData(dataStoreInfo);

//            var imageFiles = await _imageFileClient.GetAllImageFilesAsync(authorizationData, library.Id);

//            return imageFiles.Select(x => new ApiImageFile()
//            {
//                Id = x.Id,
//                Name = x.Name,
//                DataStoreInfoId = dataStoreInfo.Id,
//                Extension = x.Extension,
//                Tags = 
//            });
//        }

//        public async Task<bool> DeleteImageFileAsync(ApiImageFile imageFile)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(imageFile.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            return await _imageFileClient.DeleteImageFileAsync(dataStoreInfo, imageFile.Id);
//        }

//        public async Task UpdateImageFileAsync(ApiImageFile imageFile)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(imageFile.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            await _imageFileClient.UpdateImageFileAsync(dataStoreInfo, imageFile);
//        }

//        public async Task<Stream> GetFileContentStreamAsync(ApiImageFile imageFile)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(imageFile.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            return await _imageFileClient.GetFileAsync(dataStoreInfo, imageFile.Id);
//        }

//        public async Task UpdateFileContentAsync(ApiImageFile imageFile, Stream updatedImageFileContentStream)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(imageFile.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            await _imageFileClient.UpdateFileContentAsync(dataStoreInfo, imageFile, updatedImageFileContentStream);
//        }
//        #endregion

//        private AuthorizationData GetAuthorizationData(APIDataStoreInfo dataStoreInfo)
//        {
//            return new AuthorizationData()
//            {
//                AccessToken = dataStoreInfo.AccessToken,
//                RefreshToken = dataStoreInfo.RefreshToken,
//                ExpiryDate = dataStoreInfo.ExpiryDate,
//            };
//        }
//    }
//}
