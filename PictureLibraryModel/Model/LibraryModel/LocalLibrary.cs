namespace PictureLibraryModel.Model.LibraryModel
{
    public class LocalLibrary : Library
    {
        ~LocalLibrary()
        {
            //TODO: fix
            Icon?.Dispose();
        }
    }
}
