using PictureLibraryModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class FileSystemEntitiesFactory : IFileSystemEntitiesFactory
    {
        public Drive GetDrive(string name, IFileSystemService fileSystemService)
        {
            return new Drive(name, fileSystemService);
        }

        public Directory GetDirectory(string fullPath, string name, IFileSystemService fileSystemService)
        {
            return new Directory(fullPath, name, fileSystemService);
        }

        public Directory GetDirectory(string fullPath, ObservableCollection<object> children)
        {
            return new Directory(fullPath, children);
        }

        public Directory GetDirectory(ObservableCollection<object> children, string name)
        {
            return new Directory(children, name);
        }

        public ImageFile GetImageFile()
        {
            return new ImageFile();
        }

        public ImageFile GetImageFile(string path)
        {
            return new ImageFile(path);
        }
    }
}
