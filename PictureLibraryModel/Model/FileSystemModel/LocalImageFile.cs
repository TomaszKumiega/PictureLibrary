using System;
using System.Drawing;

namespace PictureLibraryModel.Model
{
    public class LocalImageFile : ImageFile
    {
        public override Guid? RemoteStorageInfoId => null;
        public LocalImageFile()
        {
            
        }

        public override void LoadIcon()
        {
            Icon = Image.FromFile(Path);
        }

        ~LocalImageFile()
        {
            Icon?.Dispose();
        }
    }
}
