using PictureLibraryModel.DataProviders.Queries;
using PictureLibraryModel.Model;
using System;

namespace PictureLibraryModel.DataProviders.Repositories
{
    public class ImageFileRepository : Repository<ImageFile>, IImageFileRepository
    {
        private readonly Func<ImageFileQueryBuilder> _imageFileQueryBuilderLocator;

        public ImageFileRepository(
            IDataSourceCollection dataSourceCollection,
            Func<ImageFileQueryBuilder> imageFileQueryBuilderLocator)
        {
            _dataSourceCollection = dataSourceCollection;
            _imageFileQueryBuilderLocator = imageFileQueryBuilderLocator;
        }

        public IImageFileQueryBuilder Query()
        {
            var queryBuilder = _imageFileQueryBuilderLocator();
            queryBuilder.StartQuery(_dataSourceCollection);

            return queryBuilder;
        }

        public void RemoveImage(ImageFile imageFile)
        {
            IDataSource dataSource = GetDataSource(imageFile);
            dataSource.ImageProvider.RemoveImage(imageFile);
        }

        public ImageFile AddImageToLibrary(ImageFile imageFile, Library library)
        {
            IDataSource dataSource = GetDataSource(library);
            return dataSource.ImageProvider.AddImageToLibrary(imageFile, library);
        }

    }
}
