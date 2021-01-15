using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;
using NLog;
using System.Linq;

using Directory = PictureLibraryModel.Model.Directory;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public abstract class FileSystemService : IFileProvider
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public virtual IEnumerable<Folder> GetSubFolders(string topDirectory, SearchOption option)
        {
            string[] fullPaths = null;

            try
            {
                fullPaths = System.IO.Directory.GetDirectories(topDirectory, "*", option);
            }
            catch(UnauthorizedAccessException e)
            {
                _logger.Debug(e, e.Message);
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

        public virtual void Copy(IExplorableElement entity, string destinationPath)
        {
            if (entity == null) throw new ArgumentNullException("Entity");
            if (destinationPath == null) throw new ArgumentNullException("destinationPath");
            if (destinationPath.Trim() == String.Empty) throw new ArgumentException("destinationPath");
            if (!System.IO.Directory.Exists(destinationPath)) throw new DirectoryNotFoundException(destinationPath);
            if (!destinationPath.EndsWith("\\")) destinationPath += "\\";
            if (entity.Origin != Origin.Local) throw new Exception("Cannot copy remote files");

            if (entity is Folder)
            {
                CopyDirectory(entity.FullPath, destinationPath + entity.Name);
            }
            else if(entity is ImageFile)
            {
                File.Copy(entity.FullPath, destinationPath + entity.Name);
            }
        }

        public virtual void Move(IExplorableElement entity, string destinationPath)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            if (destinationPath == null) throw new ArgumentNullException("destinationPath");
            if (destinationPath.Trim() == String.Empty) throw new ArgumentException("destinationPath");
            if (!destinationPath.EndsWith("\\")) destinationPath += "\\";
            if (entity.Origin != Origin.Local) throw new Exception("Cannot move remote files");

            if (entity is Folder)
            {
                System.IO.Directory.Move(entity.FullPath, destinationPath + entity.Name);
            }
            else if(entity is ImageFile)
            {
                File.Move(entity.FullPath, destinationPath + entity.Name);
            }
        }

        public abstract IEnumerable<Directory> GetRootDirectories();

        public virtual IEnumerable<IExplorableElement> GetDirectoryContent(string path)
        {
            var content = new List<IExplorableElement>();

            var filePaths = System.IO.Directory.GetFiles(path);
            var directoryPaths = System.IO.Directory.GetDirectories(path);

            foreach(var t in filePaths)
            {
                if(ImageFile.IsFileAnImage(t))
                {
                    var fileInfo = new FileInfo(t);

                    var imageFileBuilder = new LocalFileSystemImageFileBuilder(fileInfo);
                    var imageFileDirector = new ImageFileDirector(imageFileBuilder);
                    imageFileDirector.MakeImageFile();

                    var imageFile = imageFileDirector.GetImageFile();

                    content.Add(imageFile);
                }
            }

            foreach(var t in directoryPaths)
            {
                var directoryInfo = new DirectoryInfo(t);

                content.Add(new Folder(directoryInfo.FullName, directoryInfo.Name, this, Origin.Local));
            }

            return content;
        }

        private void CopyDirectory(string sourcePath, string destinationPath)
        {
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
                CopyDirectory(d.FullName, subDirectory.FullName);
            }
        }
    }
}
