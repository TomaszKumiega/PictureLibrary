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
            //TODO: fix
            Icon = Image.FromFile(".\\Icons\\DiskIcon.png");
        }

        ~Drive()
        {
            //TODO: fix
            Icon?.Dispose();
        }
    }
}
