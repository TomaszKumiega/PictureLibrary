using CommunityToolkit.Mvvm.ComponentModel;
using PictureLibraryModel.Model;

namespace PictureLibrary.Libraries.UI.DataViewModels
{
    public class ImageFileViewModel : ObservableObject
    {
        public ImageFileViewModel(ImageFile imageFile)
        {
            Name = imageFile.Name;
            Tags = imageFile.Tags;
            Icon = ImageSource.FromUri(new Uri(imageFile.IconUrl));
        }

        public string? Name { get; }
        public List<Tag> Tags { get; }
        public ImageSource Icon { get; }
    }
}
