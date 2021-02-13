using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public interface IFileSystemService
    {
        void Create(string path);
        void Copy(string sourcePath, string destinationPath);
        void Move(string sourcePath, string destinationPath);
        void Rename(string path, string name);
        void Remove(string path);
        bool Exists(string path);
        FileSystemInfo GetInfo(string path);
    }
}
