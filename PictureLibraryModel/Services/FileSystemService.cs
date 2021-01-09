using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;
using NLog;

using Directory = PictureLibraryModel.Model.Directory;
using PictureLibraryModel.Model;

namespace PictureLibraryModel.Services
{
    public abstract class FileSystemService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<Directory> GetDirectories(string topDirectory, SearchOption option)
        {
            if (topDirectory == null) throw new ArgumentNullException();


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

            var directories = new List<Directory>();

            if (fullPaths != null)
            {
                foreach (var t in fullPaths)
                {
                    directories.Add(new Directory(t, (new System.IO.DirectoryInfo(t)).Name, this));
                }
            }
            else
            {
                throw new Exception("Failed getting directories");
            }

            return directories;
        }

        public abstract IEnumerable<IFileSystemEntity> GetRootDirectories();
    }
}
