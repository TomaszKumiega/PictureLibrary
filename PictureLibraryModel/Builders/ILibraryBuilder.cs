using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibraryModel.Builders
{
    public interface ILibraryBuilder
    {
        ILibraryBuilder CreateLibrary(IDataStoreInfo? dataStoreInfo = null);
        ILibraryBuilder WithName(string name);
        ILibraryBuilder WithDescription(string description);
        Library GetLibrary();
    }
}
