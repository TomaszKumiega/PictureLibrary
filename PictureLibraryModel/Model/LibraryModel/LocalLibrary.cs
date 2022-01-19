namespace PictureLibraryModel.Model.LibraryModel
{
    public class LocalLibrary : Library
    {
        ~LocalLibrary()
        {
            Icon?.Dispose();
        }
    }
}
