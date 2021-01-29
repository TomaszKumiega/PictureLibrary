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
            if (destinationPath == null) throw new ArgumentNullException();
            if (!destinationPath.EndsWith("\\")) destinationPath += "\\";

            System.IO.Directory.CreateDirectory(destinationPath);

            var sourceDirectoryInfo = new DirectoryInfo(sourcePath);
            var destinationDirectoryInfo = new DirectoryInfo(destinationPath);

            foreach (FileInfo i in sourceDirectoryInfo.GetFiles())
            {
                i.CopyTo(destinationPath + i.Name, true);
            }

            foreach(DirectoryInfo d in sourceDirectoryInfo.GetDirectories())
            {
                var subDirectory = destinationDirectoryInfo.CreateSubdirectory(d.Name);
                Copy(d.FullName, subDirectory.FullName);
            }
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
            var parentDirectory = System.IO.Directory.GetParent(path).FullName;

            Move(path, parentDirectory + '\\' + name);
        }
    }
}
