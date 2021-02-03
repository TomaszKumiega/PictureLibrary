﻿using NLog;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public class DirectoryService : IDirectoryService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

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
            var content = new List<IExplorableElement>();

            var filePaths = System.IO.Directory.GetFiles(path).ToList();
            var directoryPaths = System.IO.Directory.GetDirectories(path).ToList();

            foreach (string t in filePaths)
            {
                if (ImageFile.IsFileAnImage(t) && UserHasAccessToTheFile(t))
                {
                    var fileInfo = new FileInfo(t);
                    var imageFileBuilder = new LocalFileSystemImageFileBuilder(fileInfo);
                    var imageFileDirector = new ImageFileDirector(imageFileBuilder);
                    imageFileDirector.MakeImageFile();

                    var imageFile = imageFileDirector.GetImageFile();

                    content.Add(imageFile);
                }
            }

            foreach (var t in directoryPaths)
            {
                if (UserHasAccessToTheFolder(t))
                {
                    var directoryInfo = new DirectoryInfo(t);
                    content.Add(new Folder(directoryInfo.FullName, directoryInfo.Name, this, Origin.Local));
                }
            }

            return content;
        }

        public FileSystemInfo GetInfo(string path)
        {
            return new DirectoryInfo(path);
        }

        public IEnumerable<Model.Directory> GetRootDirectories()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var rootDirectories = new List<Model.Directory>();

                foreach (var driveinfo in DriveInfo.GetDrives())
                {
                    if (System.IO.Directory.Exists(driveinfo.Name))
                    {
                        rootDirectories.Add(new Drive(driveinfo.Name, driveinfo.Name, this, Origin.Local));
                    }
                }

                return rootDirectories;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public IEnumerable<Folder> GetSubFolders(string path)
        {
            string[] fullPaths = null;

            try
            {
                fullPaths = System.IO.Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException e)
            {
                _logger.Debug(e, "UnauthorizedAccessException directory wasn't added");
            }

            var directories = new List<Folder>();

            if (fullPaths != null)
            {
                foreach (var t in fullPaths)
                {
                    directories.Add(new Folder(t, (new System.IO.DirectoryInfo(t)).Name, this, Origin.Local));
                }
            }

            return directories;
        }

        public void Move(string sourcePath, string destinationPath)
        {
            System.IO.Directory.Move(sourcePath, destinationPath);
        }

        public void Rename(string path, string name)
        {
            var parentDirectory = System.IO.Directory.GetParent(path).FullName;

            Move(path, parentDirectory + '\\' + name);
        }

        private bool UserHasAccessToTheFolder(string path)
        {
            try
            {
                System.IO.Directory.GetDirectories(path);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        private bool UserHasAccessToTheFile(string path)
        {
            try
            {
                System.IO.File.GetAttributes(path);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        public void Remove(string path)
        {
            System.IO.Directory.Delete(path);
        }

        public bool IsDirectory(string path)
        {
            FileAttributes attr = System.IO.File.GetAttributes(path);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory) return true;
            else return false;
        }
    }
}
