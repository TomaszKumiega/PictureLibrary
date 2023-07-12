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
            IconUrl = imageFile.IconUrl;
        }

        public string? Name { get; }
        public List<Tag> Tags { get; }
        public string IconUrl { get; }
    }
}
