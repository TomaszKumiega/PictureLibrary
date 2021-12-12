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
            Icon = Icon.ExtractAssociatedIcon(Path);
        }

        ~LocalImageFile()
        {
            Icon?.Dispose();
        }
    }
}
