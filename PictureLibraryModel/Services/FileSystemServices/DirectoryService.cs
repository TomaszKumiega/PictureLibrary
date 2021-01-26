using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public class DirectoryService : IDirectoryService
    {
        public void Copy(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public void Create(string path)
        {
            System.IO.Directory.CreateDirectory(path);
        }

        public IEnumerable<IExplorableElement> GetDirectoryContent(string path)
        {
            throw new NotImplementedException();
        }

        public FileSystemInfo GetInfo(string path)
        {
            return new DirectoryInfo(path);
        }

        public IEnumerable<Model.Directory> GetRootDirectories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Folder> GetSubFolders(string path)
        {
            throw new NotImplementedException();
        }

        public void Move(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public void Rename(string path, string name)
        {
            throw new NotImplementedException();
        }
    }
}
