using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.TagService;
using PictureLibrary.Infrastructure.ImplementationSelector;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess
{
    public class TagsProvider : ITagsProvider
    {
        private readonly IDataStoreInfoService _dataStoreInfoService;
        private readonly IImplementationSelector<DataStoreType, ITagService> _tagServiceSelector;

        public TagsProvider(
            IDataStoreInfoService dataStoreInfoService,
            IImplementationSelector<DataStoreType, ITagService> tagServiceSelector)
        {
            _dataStoreInfoService = dataStoreInfoService;
            _tagServiceSelector = tagServiceSelector;
        }

        public async Task<IEnumerable<Tag>> GetTagsFromLibraryAsync(Library library)
        {
            var dataStoreInfo = _dataStoreInfoService.GetDataStoreInfoFromLibrary(library);
            var dataStoreInfoType = dataStoreInfo?.Type ?? DataStoreType.Local;

            var tagService = _tagServiceSelector.Select(dataStoreInfoType);

            return await tagService.GetAllTagsAsync(library);
        }
    }
}
