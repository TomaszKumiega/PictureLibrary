using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public class Drive : Directory
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public Drive() : base()
        {

        }

        public Drive(IDirectoryService directoryService) : base(directoryService)
        {

        }

        public Drive(string path, string name, IDirectoryService directoryService, Guid origin) : base(path, name, directoryService, origin)
        {
            IconSource = "pack://application:,,,/Icons/DiskIcon.png";
        }
    }
}
