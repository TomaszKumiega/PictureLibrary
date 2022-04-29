using System;
using System.Drawing;

namespace PictureLibraryModel.Model
{
    public class LocalImageFile : ImageFile
    {
        public override Guid? RemoteStorageInfoId => null;

        public LocalImageFile() : base()
        {

        }

        public override void LoadIcon()
        {
            //TODO: fix
            //Icon = Image.FromFile(Path);
        }

        ~LocalImageFile()
        {
            //TODO: fix
            Icon?.Dispose();
        }
    }
}
