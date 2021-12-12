using PictureLibraryModel.Services.FileSystemServices;
using System.Drawing;

namespace PictureLibraryModel.Model
{
    public class Drive : Directory
    {
        public Drive(string path, string name, IDirectoryService directoryService) : base(path, name, directoryService)
        {
        }

        public override void LoadIcon()
        {
            Icon = Icon.ExtractAssociatedIcon(".\\Icons\\DiskIcon.png");
        }

        ~Drive()
        {
            Icon?.Dispose();
        }
    }
}
