using System.Drawing;

namespace PictureLibraryModel.Model
{
    public interface IExplorableElement
    {
        string Name { get; set; }
        string Path { get; set; }
        Image Icon { get; }

        void LoadIcon();
    }
}
