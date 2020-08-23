using PictureLibraryModel.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class LibraryEntitiesFactory : ILibraryEntitiesFactory
    {
        public Library GetLibrary(IFileSystemService fileSystemService)
        {
            return new Library(fileSystemService);
        }

        public Library GetLibrary(string fullPath, string name, IFileSystemService fileSystemService)
        {
            return new Library(fullPath, name, fileSystemService);
        }

        public Library GetLibrary(string fullPath, string name, List<Album> albums, IFileSystemService fileSystemService)
        {
            return new Library(fullPath, name, albums, fileSystemService);
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
