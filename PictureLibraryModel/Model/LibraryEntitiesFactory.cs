using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class LibraryEntitiesFactory : ILibraryEntitiesFactory
    {
        public Library GetLibrary(string fullPath, string name)
        {
            return new Library(fullPath, name);
        }

        public Library GetLibrary(string fullPath, string name, List<Album> albums)
        {
            return new Library(fullPath, name, albums);
        }

        public ImageFile GetImageFile(string path)
        {
            return new ImageFile(path);
        }

        public ImageFile GetImageFile()
        {
            return new ImageFile();
        }

        public Album GetAlbum(string name)
        {
            return new Album(name);
        }

        public Album GetAlbum(string name, List<ImageFile> images)
        {
            return new Album(name, images);
        }
    }
}
