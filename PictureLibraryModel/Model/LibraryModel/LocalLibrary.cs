using ImageMagick;

namespace PictureLibraryModel.Model
{
    public class LocalLibrary : Library
    {
        public override void LoadIcon()
        {
            var settings = new MagickReadSettings();
            settings.Width = 50;
            settings.Height = 50;

            Icon = new MagickImage(".\\Icons\\LibraryIcon.png", settings);
        }

        ~LocalLibrary()
        {
            Icon?.Dispose();
        }
    }
}
