using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.Libraries.UI.DataViewModels
{
    public class DataStoreInfoItemViewModel
    {
        public IDataStoreInfo? DataStoreInfo { get; }

        public DataStoreInfoItemViewModel(IDataStoreInfo? dataStoreInfo = null)
        {
            DataStoreInfo = dataStoreInfo;
        }

        public override string ToString()
        {
            return DataStoreInfo?.Name ?? "Lokalny";
        }
    }
}
