using PictureLibrary.DataAccess.DataSource;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Queries.ImageFileQuery
{
    public class ImageFileQueryBuilder : QueryBuilder<IImageFileQueryBuilder, ImageFile>, IImageFileQueryBuilder
    {
        public ImageFileQueryBuilder(
            Func<StorageQuery> storageQueryLocator)
            : base(storageQueryLocator)
        {

        }

        public IImageFileQueryBuilder StartQuery(IDataSourceCollection dataSourceCollection)
        {
            _dataSourceCollection = dataSourceCollection;

            return this;
        }

        public override List<ImageFile> ToList()
        {
            EnsureQueryWasStarted();

            List<ImageFile> imageFiles = new();

            return imageFiles;
        }
    }
}
