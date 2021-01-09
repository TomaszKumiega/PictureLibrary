using PictureLibraryModel.Services;
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

        public Drive(string path, string name, FileSystemService fileSystemService) : base(path, name, fileSystemService)
        {
            Icon = new Bitmap("Icons/DiskIcon.png");
        }

        ~Drive()
        {
            if(Icon != null)
            {
                Icon.Dispose();
            }
        }
    }
}
