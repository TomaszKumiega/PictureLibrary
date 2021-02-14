﻿using NLog;
using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using File = System.IO.File;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public class FileService : FileSystemService, IFileService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public override void Copy(string sourcePath, string destinationPath)
        {
            File.Copy(sourcePath, destinationPath);
        }

        public override void Create(string path)
        {
            File.Create(path);
        }

        public override bool Exists(string path)
        {
            return File.Exists(path);
        }

        public List<string> FindFiles(string searchPattern)
        {
            throw new NotImplementedException();
        }

        public List<string> FindFiles(string searchPattern, string directory)
        {
            throw new NotImplementedException();
        }

        public override FileSystemInfo GetInfo(string path)
        {
            return new FileInfo(path);
        }

        public override void Move(string sourcePath, string destinationPath)
        {
            File.Move(sourcePath, destinationPath);
        }

        public Stream OpenFile(string path)
        {
            return File.Open(path, FileMode.Open);
        }

        public override void Remove(string path)
        {
            File.Delete(path);
        }

        public override void Rename(string path, string name)
        {
            var extension = new FileInfo(path).Extension;
            var directoryPath = System.IO.Directory.GetParent(path).FullName;

            File.Move(path, directoryPath + "\\" + name + extension);
        }
    }
}
