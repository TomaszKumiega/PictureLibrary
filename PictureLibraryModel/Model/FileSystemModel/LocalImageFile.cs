using ImageMagick;
using System;

namespace PictureLibraryModel.Model
{
    public class LocalImageFile : ImageFile
    {
        public LocalImageFile() : base()
        {

        }

        public override void LoadIcon()
        {
            var settings = new MagickReadSettings();
            settings.Width = 50;
            settings.Height = 50;

            Icon = new MagickImage(Path, settings);
        }

        ~LocalImageFile()
        {
            Icon?.Dispose();
        }
    }
}
