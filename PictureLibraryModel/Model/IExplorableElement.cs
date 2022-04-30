using ImageMagick;

namespace PictureLibraryModel.Model
{
    public interface IExplorableElement
    {
        string Name { get; set; }
        string Path { get; set; }
        MagickImage Icon { get; }

        void LoadIcon();
    }
}
