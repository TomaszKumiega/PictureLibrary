using PictureLibrary.DataAccess.DataSource;
using PictureLibraryModel.Model;

namespace PictureLibrary.DataAccess.Repositories.ImageFileRepository
{
    public class ImageFileRepository : Repository<ImageFile>, IImageFileRepository
    {
        public ImageFileRepository(
            IDataSourceCollection dataSourceCollection)
            : base(dataSourceCollection)
        {
        }

        public void RemoveImage(ImageFile imageFile)
        {
            IDataSource dataSource = GetDataSource(imageFile);
            dataSource.ImageProvider!.RemoveImage(imageFile);
        }

        public ImageFile AddImageToLibrary(ImageFile imageFile, Library library)
        {
            IDataSource dataSource = GetDataSource(library);
            return dataSource.ImageProvider!.AddImageToLibrary(imageFile, library);
        }
    }
}
