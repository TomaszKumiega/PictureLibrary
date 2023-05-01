﻿using PictureLibrary.APIClient.ImageFileClient;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;

namespace PictureLibrary.DataAccess.ImageFileService
{
    public class PictureLibraryApiImageFileService
    {
        private readonly IImageFileClient _imageFileClient;
        private readonly IDataStoreInfoProvider _dataStoreInfoProvider;

        public PictureLibraryApiImageFileService(
            IImageFileClient imageFileClient,
            IDataStoreInfoProvider dataStoreInfoProvider)
        {
            _imageFileClient = imageFileClient;
            _dataStoreInfoProvider = dataStoreInfoProvider;
        }


        public async Task<IEnumerable<ApiImageFile>> GetApiImageFilesAsync(ApiLibrary library)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId);
            return await _imageFileClient.GetAllImageFilesAsync(dataStoreInfo, library.Id);
        }

        public async Task<bool> DeleteImageFileAsync(ApiImageFile imageFile)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(imageFile.DataStoreInfoId);
            return await _imageFileClient.DeleteImageFileAsync(dataStoreInfo, imageFile.Id);
        }

        public async Task UpdateImageFileAsync(ApiImageFile imageFile)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(imageFile.DataStoreInfoId);
            await _imageFileClient.UpdateImageFileAsync(dataStoreInfo, imageFile);
        }

        public async Task<Stream> GetFileContentStreamAsync(ApiImageFile imageFile)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(imageFile.DataStoreInfoId);
            return await _imageFileClient.GetFileAsync(dataStoreInfo, imageFile.Id);
        }

        public async Task UpdateFileContentAsync(ApiImageFile imageFile, Stream updatedImageFileContentStream)
        {
            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(imageFile.DataStoreInfoId);
            await _imageFileClient.UpdateFileContentAsync(dataStoreInfo, imageFile, updatedImageFileContentStream);
        }
    }
}
