using ImageMagick;
using PictureLibraryModel.Services.FileSystemServices;

namespace PictureLibraryModel.Model
{
    public class Drive : Directory
    {
        public Drive(string path, string name, IDirectoryService directoryService) : base(path, name, directoryService)
        {
        }

        public override void LoadIcon()
        {
            var settings = new MagickReadSettings();
            settings.Width = 50;
            settings.Height = 50;

            Icon = new MagickImage(".\\Icons\\DiskIcon.png", settings);
        }

        ~Drive()
        {
            Icon?.Dispose();
        }
    }
}
