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

        public Drive(string path, string name, IFileProvider fileProvider, Origin origin) : base(path, name, fileProvider, origin)
        {
            InitializeIcon();
        }

        private void InitializeIcon()
        {
            try
            {
                Icon = new Bitmap("Icons/DiskIcon.png");
            }
            catch(Exception e)
            {
                _logger.Error(e, "Couldn't load disk icon");
            }
        }

        ~Drive()
        {
            Icon?.Dispose();
        }
    }
}
