using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;

namespace PictureLibraryModel.DataProviders.Queries
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
            List<ImageFile> imageFiles = new();


            return imageFiles;
        }
    }
}
