using System.Collections.Generic;

namespace PictureLibraryModel.Model
{
    public interface ILibraryEntitiesFactory
    {
        Album GetAlbum(string name);
        Album GetAlbum(string name, List<ImageFile> images);
        ImageFile GetImageFile();
        ImageFile GetImageFile(string path);
        Library GetLibrary(string fullPath, string name);
        Library GetLibrary(string fullPath, string name, List<Album> albums);
    }
}