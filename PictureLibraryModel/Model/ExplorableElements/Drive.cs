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
        public Drive() : base()
        {

        }

        public Drive(IDirectoryService directoryService) : base(directoryService)
        {

        }

        public Drive(string path, string name, IDirectoryService directoryService) : base(path, name, directoryService)
        {
            IconSource = "pack://application:,,,/Icons/DiskIcon.png";
        }
    }
}
