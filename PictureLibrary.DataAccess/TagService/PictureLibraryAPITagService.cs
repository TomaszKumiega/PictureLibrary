//using PictureLibrary.APIClient.TagClient;
//using PictureLibrary.DataAccess.DataStoreInfos;
//using PictureLibrary.DataAccess.Exceptions;
//using PictureLibraryModel.Model;
//using PictureLibraryModel.Model.DataStoreInfo;

//namespace PictureLibrary.DataAccess.TagService
//{
//    public class PictureLibraryAPITagService
//    {
//        #region Private fields
//        private readonly ITagClient _tagClient;
//        private readonly IDataStoreInfoService _dataStoreInfoProvider;
//        #endregion

//        public PictureLibraryAPITagService(
//            ITagClient tagClient,
//            IDataStoreInfoService dataStoreInfoProvider)
//        {
//            _tagClient = tagClient;
//            _dataStoreInfoProvider = dataStoreInfoProvider;
//        }

//        #region Public methods
//        public async Task<Tag> AddTagAsync(ApiLibrary library, Tag tag)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            return await _tagClient.AddTagAsync(dataStoreInfo, tag, library.Id);
//        }

//        public async Task<IEnumerable<Tag>> GetAllTagsAsync(ApiLibrary library)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            return await _tagClient.GetAllTagsAsync(dataStoreInfo, library.Id);
//        }

//        public async Task<bool> DeleteTagAsync(ApiLibrary library, Tag tag)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            return await _tagClient.DeleteTagAsync(dataStoreInfo, tag.Id);
//        }

//        public async Task<bool> UpdateTagAsync(ApiLibrary library, Tag tag)
//        {
//            var dataStoreInfo = _dataStoreInfoProvider.GetDataStoreInfo<APIDataStoreInfo>(library.DataStoreInfoId) ?? throw new PictureLibraryApiAccountConfigurationNotFoundException();
//            return await _tagClient.UpdateTagAsync(dataStoreInfo, tag);
//        }
//        #endregion
//    }
//}
