using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;
using NLog;
using System.Linq;

using Directory = PictureLibraryModel.Model.Directory;
using PictureLibraryModel.Model;

namespace PictureLibraryModel.Services
{
    public abstract class FileSystemService : IFileProvider
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public virtual IEnumerable<Folder> GetSubFolders(string topDirectory, SearchOption option)
        {
            if (topDirectory == null) throw new ArgumentNullException();
            if (topDirectory.Trim() == String.Empty) throw new ArgumentException();
            if (!System.IO.Directory.Exists(topDirectory)) throw new DirectoryNotFoundException("Directory: " + topDirectory + " not found");


            string[] fullPaths = null;

            try
            {
                fullPaths = System.IO.Directory.GetDirectories(topDirectory, "*", option);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Couldn't load directories from " + topDirectory);
            }

            var directories = new List<Folder>();

            if (fullPaths != null)
            {
                foreach (var t in fullPaths)
                {
                    directories.Add(new Folder(t, (new System.IO.DirectoryInfo(t)).Name, this, Origin.Local));
                }
            }
            else
            {
                throw new Exception("Failed getting directories");
            }

            return directories;
        }

        public virtual void Copy(IFileSystemEntity entity, string destinationPath)
        {
            if (entity == null) throw new ArgumentNullException("Entity");
            if (destinationPath == null) throw new ArgumentNullException("destinationPath");
            if (destinationPath.Trim() == String.Empty) throw new ArgumentException("destinationPath");
            if (!System.IO.Directory.Exists(destinationPath)) throw new DirectoryNotFoundException(destinationPath);
            if (!destinationPath.EndsWith("\\")) destinationPath += "\\";

            if (entity is Folder)
            {
                CopyDirectory(entity.FullPath, destinationPath + entity.Name);
            }
            else if(entity is ImageFile)
            {
                File.Copy(entity.FullPath, destinationPath + entity.Name);
            }
        }

        public virtual void Move(IFileSystemEntity entity, string destinationPath)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            if (destinationPath == null) throw new ArgumentNullException("destinationPath");
            if (destinationPath.Trim() == String.Empty) throw new ArgumentException("destinationPath");
            if (!destinationPath.EndsWith("\\")) destinationPath += "\\";

            if(entity is Folder)
            {
                System.IO.Directory.Move(entity.FullPath, destinationPath + entity.Name);
            }
            else if(entity is ImageFile)
            {
                File.Move(entity.FullPath, destinationPath + entity.Name);
            }
        }

        public abstract IEnumerable<Directory> GetRootDirectories();

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
